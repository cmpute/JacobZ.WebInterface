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
        /// <summary>
        /// 条目编号
        /// </summary>
        public uint ID { get; set; }
        
        /// <summary>
        /// 条目类型
        /// </summary>
        public SubjectType Type { get; set; }

        /// <summary>
        /// 条目标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 条目中文标题
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 条目简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 条目章节列表
        /// </summary>
        public IReadOnlyList<Episode> Episodes { get; set; }

        /// <summary>
        /// 放送/上线时间
        /// </summary>
        public DateTimeOffset AirDate { get; set; }

        /// <summary>
        /// 放松星期
        /// </summary>
        public DayOfWeek AirWeekday { get; set; }

        /// <summary>
        /// 评分。数组给出了不同分数打分的人数
        /// </summary>
        public int[] Rating { get; set; }

        /// <summary>
        /// 在同类条目中的评分排名
        /// </summary>
        public uint Rank { get; set; }

        /// <summary>
        /// 条目图像
        /// </summary>
        public ImageSource Images { get; set; }

        /// <summary>
        /// 条目收藏统计。字典给出了不同收藏状态的人数
        /// </summary>
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

    /// <summary>
    /// 代表章节或集
    /// </summary>
    public class Episode
    {
        /// <summary>
        /// 该集编号
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 该集类型
        /// </summary>
        public EpisodeType Type { get; set; }

        /// <summary>
        /// 该集的集号
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 该集的标题/名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 该集的中文标题/名称
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 该集时长
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 该集放送日期
        /// </summary>
        public DateTimeOffset AirDate { get; set; }

        /// <summary>
        /// 该集评论数目
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 该集的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 该集的放松状态
        /// </summary>
        public EpisodeStatus Status { get; set; }
    }

    /// <summary>
    /// 代表番组某集的类型
    /// </summary>
    public enum EpisodeType
    {
        /// <summary>
        /// 本番
        /// </summary>
        Main,
        /// <summary>
        /// 番外/特典
        /// </summary>
        SP
    }

    /// <summary>
    /// 代表番组某集的放送状态
    /// </summary>
    public enum EpisodeStatus
    {
        /// <summary>
        /// 未知/未放送
        /// </summary>
        NA,
        /// <summary>
        /// 已放送
        /// </summary>
        Air,
        /// <summary>
        /// 今日放送
        /// </summary>
        Today
    }

    /// <summary>
    /// 图像来源类
    /// </summary>
    public class ImageSource
    {
        /// <summary>
        /// 大图地址
        /// </summary>
        public string Large { get; set; }

        /// <summary>
        /// 常规大小图的地址
        /// </summary>
        public string Common { get; set; }

        /// <summary>
        /// 中图地址
        /// </summary>
        public string Medium { get; set; }

        /// <summary>
        /// 小图地址
        /// </summary>
        public string Small { get; set; }

        /// <summary>
        /// 用于网格显示的图的地址
        /// </summary>
        public string Grid { get; set; }
    }

    /// <summary>
    /// 代表收藏条目
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// 收藏评分
        /// </summary>
        public uint Rating { get; set; }

        /// <summary>
        /// 收藏吐槽
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 收藏标签
        /// </summary>
        public List<string> Tag { get; set; }

        /// <summary>
        /// 观看到的最新集数
        /// </summary>
        public int? EpStatus { get; set; }
        
        /// <summary>
        /// 上次更新收藏状态的时间
        /// </summary>
        public DateTimeOffset? LastTouch { get; set; }

        /// <summary>
        /// 收藏对象所属的用户
        /// </summary>
        public User User { get; set; }
    }

    /// <summary>
    /// 代表收藏状态
    /// </summary>
    public enum CollectionStatus
    {
        /// <summary>
        /// 想看/想读/想听
        /// </summary>
        Wish,
        /// <summary>
        /// 看过/读过/听过
        /// </summary>
        Collect,
        /// <summary>
        /// 在看/在读/在听
        /// </summary>
        Doing,
        /// <summary>
        /// 搁置
        /// </summary>
        OnHold,
        /// <summary>
        /// 抛弃
        /// </summary>
        Dropped
    }

    /// <summary>
    /// 代表番组角色
    /// </summary>
    public class Character
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色类型名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色中文名
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 角色图像
        /// </summary>
        public ImageSource Images { get; set; }

        /// <summary>
        /// 角色评论数
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 角色收藏数
        /// </summary>
        public int CollectCount { get; set; }

        /// <summary>
        /// 角色人物信息
        /// </summary>
        public PersonInformation Information { get; set; }
    }

    /// <summary>
    /// 代表一个别名/马甲名
    /// </summary>
    public class AliasName
    {
        /// <summary>
        /// 别名类型
        /// </summary>
        public AliasNameType Type { get; set; }

        /// <summary>
        /// 别名名字
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 代表别名类型
    /// </summary>
    public enum AliasNameType
    {
        /// <summary>
        /// 假名
        /// </summary>
        Kana,
        /// <summary>
        /// 罗马音
        /// </summary>
        Romaji,
        /// <summary>
        /// 日语名
        /// </summary>
        JP
    }

    /// <summary>
    /// 代表一个现实人物
    /// </summary>
    public class Person
    {
        /// <summary>
        /// 人物编号
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 人物名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 人物中文名
        /// </summary>
        public string ChineseName { get; set; }

        /// <summary>
        /// 人物图像
        /// </summary>
        public ImageSource Images { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 收藏数
        /// </summary>
        public int CollectCount { get; set; }

        /// <summary>
        /// 人物信息
        /// </summary>
        public PersonInformation Information { get; set; }
    }

    /// <summary>
    /// 代表人物的额外信息
    /// </summary>
    public class PersonInformation
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string Birthday { get; set; } // TODO: 可考虑转换为DateTime

        /// <summary>
        /// 人物别名
        /// </summary>
        public IReadOnlyList<AliasName> Aliases { get; set; }

        /// <summary>
        /// 人物性别
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// 三维
        /// </summary>
        public int[] BWH { get; set; }

        /// <summary>
        /// 信息来源
        /// </summary>
        public string Source { get; set; }
    }

    /// <summary>
    /// 讨论版留言
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// 讨论的编号
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 讨论标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 关联条目ID
        /// </summary>
        public uint MainID { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// 讨论上次更新的日期
        /// </summary>
        public DateTimeOffset LastPost { get; set; }

        /// <summary>
        /// 回复数
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 发起用户
        /// </summary>
        public User User { get; set; }
    }

    /// <summary>
    /// 代表bangumi.tv的用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public ImageSource Avatar { get; set; }

        /// <summary>
        /// 用户签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 用户认证字符串
        /// </summary>
        public string Authentication { get; set; }
    }

    /// <summary>
    /// （长篇）评论
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 评论标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 评论内容简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 评论中的第一张图的地址
        /// </summary>
        public string ThumbImage { get; set; } // XXX: 第一张图的链接，考虑略去？

        /// <summary>
        /// 回复数
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        // 这个字段可以从Timestamp生成
        // public string DateLine { get; set; }

        /// <summary>
        /// 发布的用户
        /// </summary>
        public User User { get; set; }
    }
}
