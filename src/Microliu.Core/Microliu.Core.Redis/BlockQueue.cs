using System;
using System.Collections.Generic;
using System.Threading;

namespace Microliu.Core.Redis
{
    public class BlockQueue<T>
    {
        private Queue<T> _queue = null;

        private ManualResetEvent _dequeue_wait = null;

        private bool _isShutDown = false;

        public int Count { get { return _queue.Count; } }

        public void ShutDown()
        {
            _isShutDown = true;
            _dequeue_wait.Set();
        }


        public BlockQueue(int capacity = -1)
        {
            _queue = (capacity == -1 ? new Queue<T>() : new Queue<T>(capacity));
            _dequeue_wait = new ManualResetEvent(false);
        }

        public void EnQueue(T item)
        {
            if (_isShutDown == true) throw new InvalidOperationException("服务器未开启.[EnQueue]");
            lock (_queue)
            {
                _queue.Enqueue(item);
                _dequeue_wait.Set();
            }
        }

        public T DeQueue(int waitTime)
        {
            bool queueEmpty = false;
            T item = default(T);
            while (true)
            {
                lock (_queue)
                {
                    if (Count > 0)
                    {
                        item = _queue.Dequeue();
                        _dequeue_wait.Reset();
                    }
                    else
                    {
                        if (_isShutDown == true) throw new InvalidOperationException("服务器未开启.[EnQueue]");
                        else queueEmpty = true;
                    }
                }
                if (item != null) return item;
                if (queueEmpty) _dequeue_wait.WaitOne(waitTime);
            }
        }

        public void Clear()
        {
            _queue.Clear();
        }

    }
}
