using System;
using System.Collections.Generic;

namespace Microliu.Core.RedisCache
{
    public interface ICacheService
    {
        #region 字符串操作

        /// <summary>
        /// 获取指定键的值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        string StringGet(string key);

        /// <summary>
        /// 获取存储在键上的字符串的子字符串
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        string StringGet(string key, int start, int end);

        /// <summary>
        /// 设置键的字符串值并返回旧值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        string StringGetSet(string key, string value);

        /// <summary>
        /// 返回在键处存储的字符串值编译处的位值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        bool StringGetBit(string key, long offset);

        /// <summary>
        /// 获取所有给定键的值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        List<string> StringMultiGet(params string[] keys);

        /// <summary>
        /// 存储在键上的字符串值中设置或清除偏移处的位
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        bool StringSetBit(string key, long offset);

        /// <summary>
        /// 设置键和值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool StringSet(string key, string value);


        /// <summary>
        /// 设置键和值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool StringSet(string key, string value, TimeSpan expiry);

        /// <summary>
        /// 设置键和值，仅当键不存在时
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        void StringSetIfNull(string key, string value, TimeSpan expiry);

        /// <summary>
        /// 获取存储键中的值的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long StringSize(string key);

        /// <summary>
        /// 批量设置键和值
        /// </summary>
        /// <param name="keyValues"></param>
        void StringMultiSet(Dictionary<string, string> keyValues);

        /// <summary>
        /// 将键的整数值按给定的数值增加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long StringIncrement(string key, long value = 1);

        /// <summary>
        /// 给指定键的值减少指定的value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long StringDecrement(string key, long value = 1);

        /// <summary>
        /// 在key键对应值后追加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long StringAppend(string key, string value);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool StringDelete(string key);

        /// <summary>
        /// 键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyExists(string key);
        /// <summary>
        /// 设置键的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool KeyExpire(string key, TimeSpan expiry);

        /// <summary>
        /// 设置键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool StringSet<T>(string key, T value);

        /// <summary>
        /// 获取键的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T StringGet<T>(string key);

        /// <summary>
        /// 获取指定键的剩余缓存时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TimeSpan? GetExpiry(string key);
        #endregion


        #region 哈希、散列、字典操作

        /// <summary>
        /// 删除指定的哈希字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        bool HashDelete(string key, string field);

        /// <summary>
        /// 判断是否存在散列字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        bool HashKeyExists(string key, string field);

        /// <summary>
        /// 获取存储在指定键的哈希字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        object HashGet(string key, string field);
        /// <summary>
        /// 获取指定键的哈希中所有字段和值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Dictionary<string, object> HashGetAll(string key);
        /// <summary>
        /// 获取哈希中的所有字段
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string[] HashKeys(string key);

        /// <summary>
        /// 获取散列中的字段数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long HashSize(string key);

        /// <summary>
        /// 为多个哈希字段分别设置他们的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        void HashSet(string key, Dictionary<string, string> dic);
        #endregion



         void SetDbIndex(int index);
    }
}
