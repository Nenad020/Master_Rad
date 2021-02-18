using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Communication
{
	public class WCFRequestsQueue<T> : IDisposable
	{
		public delegate Task RequestHandler(T obj);

		private readonly RequestHandler requestHandler;

		private readonly SemaphoreSlim pausingSemaphore;

		private readonly ConcurrentQueue<T> requests;

		private readonly int dequeueDelay;

		private bool doRead;

		private bool isPaused;

		public WCFRequestsQueue(RequestHandler requestHandler, uint dequeueDelay)
		{
			requests = new ConcurrentQueue<T>();
			this.requestHandler = requestHandler;
			this.dequeueDelay = (int)dequeueDelay;
			pausingSemaphore = new SemaphoreSlim(1);
			doRead = false;
			isPaused = false;
		}

		public void Enqueue(T obj)
		{
			requests.Enqueue(obj);
		}

		public void StartReadingThread()
		{
			if (doRead)
			{
				return;
			}

			doRead = true;
			ThreadPool.QueueUserWorkItem(state => QueueReadingThread());
		}

		public void StopReading()
		{
			if (!doRead) return;
			UnPause();
			doRead = false;
		}

		public void Pause()
		{
			isPaused = true;
		}

		public void UnPause()
		{
			if (!isPaused) return;

			isPaused = false;
			pausingSemaphore.Release();
		}

		private void QueueReadingThread()
		{
			while (doRead)
			{
				Thread.Sleep(dequeueDelay);
				if (isPaused)
				{
					pausingSemaphore.Wait();
				}

				if (requests.Count == 0)
				{
					continue;
				}

				if (!TryHandleRequest())
				{
					continue;
				}
			}

			foreach (var request in requests)
			{
				requestHandler(request);
			}
		}

		private bool TryHandleRequest()
		{
			T request;
			if (!requests.TryPeek(out request))
			{
				return false;
			}

			var t = requestHandler(request);
			t.Wait();

			if (!t.IsCompleted)
			{
				return false;
			}

			DequeueHandledRequest();
			return true;
		}

		private void DequeueHandledRequest()
		{
			T req;
			while (!requests.TryDequeue(out req))
			{
				Thread.Sleep(100);
			}
		}

		public void Dispose()
		{
			StopReading();
			pausingSemaphore.Dispose();
		}
	}
}