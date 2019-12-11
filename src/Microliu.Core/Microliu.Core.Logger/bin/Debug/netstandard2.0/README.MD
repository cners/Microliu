
描述：
	用于基于.NET Core实现的Web日志记录，支持服务器本地和网络分布式记录两种。
	支持Trace、Debug、Information、Warn、Error这五种级别。
	自动记录当前时间[{DateTime.Now.ToString(yyyy-MM-dd HH:mm:ss.fff)}]。
	支持添加参考标记Tags，方便分布式日志实时搜索。
	服务器本地日志采用NLog，分布式日志记录采用Exceptionless。

技术支持：
	开发人员：刘  壮
	邮    箱：liuzhuang@6iuu.com