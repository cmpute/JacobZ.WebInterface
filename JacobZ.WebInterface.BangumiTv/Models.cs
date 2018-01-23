using System;
using System.Collections.Generic;
using System.Text;

namespace JacobZ.WebInterface.BangumiTv
{
    /// <summary>
    /// bangumi.tv的条目对象
    /// </summary>
    public class Subject
    {
        public uint ID { get; set; }
        
        public SubjectType Type { get; set; }

        public string Name { get; set; }

        public string ChineseName { get; set; }

        public string Summary { get; set; }

        public IReadOnlyList<Episode> Episodes { get; set; }

        public DateTimeOffset AirDate { get; set; }

        public DayOfWeek AirWeekday { get; set; }

        public int[] Rating { get; set; }

        public uint Rank { get; set; }

        public ImageSource Images { get; set; }

        public IReadOnlyDictionary<CollectionStatus, uint> CollectionStats { get; set; }

        /// <summary>
        /// 角色列表，键值为角色对应的演员/声优列表
        /// </summary>
        public IReadOnlyDictionary<Character, IReadOnlyList<Person>> Characters { get; set; }

        /// <summary>
        /// 制作团队，键值为在本作品中的职务
        /// </summary>
        public IReadOnlyDictionary<Person, IReadOnlyList<string>> Staffs { get; set; }

        public IReadOnlyList<Topic> Topics { get; set; }
    }

    /// <summary>
    /// Bangumi条目对应的类型
    /// </summary>
    public enum SubjectType
    {
        Books = 1, // 漫画、小说
        Animation = 2, // 动画（二次元番）
        Music = 3, // 音乐
        Game = 4, // 游戏
        Drama = 6 // 三次元番
    }

    public class Episode
    {
        public uint ID { get; set; }

        public EpisodeType Type { get; set; }

        public string Sort { get; set; }

        public string Name { get; set; }

        public string NameCN { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTimeOffset AirDate { get; set; }

        public int CommentCount { get; set; }

        public string Description { get; set; }

        public EpisodeStatus Status { get; set; }
    }

    public enum EpisodeType
    {
        Main,
        SP
    }

    public enum EpisodeStatus
    {
        NA,
        Air,
        Today
    }

    public class ImageSource
    {
        public string Large { get; set; }

        public string Common { get; set; }

        public string Medium { get; set; }

        public string Small { get; set; }

        public string Grid { get; set; }
    }

    public class Collection
    {
        public uint Rating { get; set; }

        public string Comment { get; set; }

        public List<string> Tag { get; set; }

        public int? EpStatus { get; set; }
        
        public DateTimeOffset? LastTouch { get; set; }

        public User User { get; set; }
    }

    public enum CollectionStatus
    {
        Wish,
        Collect,
        Doing,
        OnHold,
        Dropped
    }

    public class Character
    {
        public uint ID { get; set; }

        public string Name { get; set; }

        public string RoleName { get; set; }

        public string ChineseName { get; set; }

        public ImageSource Images { get; set; }

        public int CommentCount { get; set; }

        public int CollectCount { get; set; }

        public PersonInformation Information { get; set; }
    }

    public class AliasName
    {
        public AliasNameType Type { get; set; }

        public string Name { get; set; }
    }

    public enum AliasNameType
    {
        Kana,
        Romaji,
        JP
    }

    public class Person
    {
        public uint ID { get; set; }

        public string Name { get; set; }

        public string ChineseName { get; set; }

        public ImageSource Images { get; set; }

        public int CommentCount { get; set; }

        public int CollectCount { get; set; }

        public PersonInformation Information { get; set; }
    }

    public class PersonInformation
    {
        public string Birthday { get; set; } // TODO: 可考虑转换为DateTime

        public IReadOnlyList<AliasName> Aliases { get; set; }

        public bool? Gender { get; set; }
    }

    /// <summary>
    /// 讨论版留言
    /// </summary>
    public class Topic
    {
        public uint ID { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// 关联条目ID
        /// </summary>
        public uint MainID { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public DateTimeOffset LastPost { get; set; }

        public int ReplyCount { get; set; }

        public User User { get; set; }
    }

    public class User
    {
        public uint ID { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public ImageSource Avatar { get; set; }

        public string Sign { get; set; }

        public string Authentication { get; set; }
    }

    /// <summary>
    /// （长篇）评论
    /// </summary>
    public class Blog
    {
        public uint ID { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string ThumbImage { get; set; } // XXX: 第一张图的链接，考虑略去？

        public int ReplyCount { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        // 这个字段可以从Timestamp生成
        // public string DateLine { get; set; }

        public User User { get; set; }
    }
}
