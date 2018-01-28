using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [JsonConverter(typeof(StringEnumConverter))]
        public SubjectType Type { get; set; }

        /// <summary>
        /// 条目标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 条目中文标题
        /// </summary>
        [JsonProperty("name_cn")]
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
        /// 条目章节数
        /// </summary>
        public int? EpisodeCount { get; set; }

        /// <summary>
        /// 放送/上线日期
        /// </summary>
        [JsonProperty("air_date")]
        public DateTime AirDate { get; set; }

        /// <summary>
        /// 放送星期
        /// </summary>
        [JsonProperty("air_weekday")]
        [JsonConverter(typeof(DayOfWeekConverter))]
        public DayOfWeek AirWeekday { get; set; }

        /// <summary>
        /// 评分。数组给出了不同分数打分的人数，数组中分数从为10到1
        /// </summary>
        [JsonConverter(typeof(RatingConverter))]
        public uint[] Rating { get; set; }

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
        [JsonProperty("collection")]
        public IReadOnlyDictionary<CollectionStatus, uint> CollectionStats { get; set; }

        /// <summary>
        /// 角色列表，键值为角色对应的演员/声优列表
        /// </summary>
        [JsonProperty("crt")]
        [JsonConverter(typeof(ObjectToPropertyDictionaryConverter), "actors")]
        public IReadOnlyDictionary<Character, IReadOnlyList<Person>> Characters { get; set; }

        /// <summary>
        /// 制作团队，键值为在本作品中的职务
        /// </summary>
        [JsonProperty("staff")]
        [JsonConverter(typeof(ObjectToPropertyDictionaryConverter), "jobs")]
        public IReadOnlyDictionary<Person, IReadOnlyList<string>> Staffs { get; set; }

        /// <summary>
        /// 讨论版留言列表
        /// </summary>
        [JsonProperty("topic")]
        public IReadOnlyList<Topic> Topics { get; set; }

        /// <summary>
        /// 日志文章列表
        /// </summary>
        [JsonProperty("blog")]
        public IReadOnlyList<Blog> Blogs { get; set; }
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
        public int Sort { get; set; }

        /// <summary>
        /// 该集的标题/名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 该集的中文标题/名称
        /// </summary>
        [JsonProperty("name_cn")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 该集时长
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// 该集放送日期
        /// </summary>
        public DateTime AirDate { get; set; }

        /// <summary>
        /// 该集评论数目
        /// </summary>
        [JsonProperty("comment")]
        public int CommentCount { get; set; }

        /// <summary>
        /// 该集的描述
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }

        /// <summary>
        /// 该集的放送状态
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
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
        [JsonProperty("large")]
        public string Large { get; set; }

        /// <summary>
        /// 常规大小图的地址
        /// </summary>
        [JsonProperty("common")]
        public string Common { get; set; }

        /// <summary>
        /// 中图地址
        /// </summary>
        [JsonProperty("medium")]
        public string Medium { get; set; }

        /// <summary>
        /// 小图地址
        /// </summary>
        [JsonProperty("small")]
        public string Small { get; set; }

        /// <summary>
        /// 用于网格显示的图的地址
        /// </summary>
        [JsonProperty("grid")]
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
        On_Hold, // XXX: 这里多个下划线是为了便于Enum.Parse，在Json.Net中无法在序列化为字典键值时自定义转换方法
        /// <summary>
        /// 抛弃
        /// </summary>
        Dropped
    }

    /// <summary>
    /// 代表番组角色
    /// </summary>
    public class Character : Person
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [JsonProperty("role_name")]
        [JsonConverter(typeof(RoleTypeConverter))]
        public RoleType Role { get; set; }
    }

    /// <summary>
    /// 代表角色类型
    /// </summary>
    public enum RoleType
    {
        /// <summary>
        /// 主角
        /// </summary>
        Lead,
        /// <summary>
        /// 配角
        /// </summary>
        Supporting,
        /// <summary>
        /// 客串
        /// </summary>
        Guest
    }

    /// <summary>
    /// 代表别名类型
    /// </summary>
    public enum AliasType
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
        JP,
        /// <summary>
        /// 昵称
        /// </summary>
        Nick,
        /// <summary>
        /// 自定义类型
        /// </summary>
        Misc
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
        [JsonProperty("name_cn")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 人物图像
        /// </summary>
        public ImageSource Images { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        [JsonProperty("comment")]
        public int CommentCount { get; set; }

        /// <summary>
        /// 收藏数
        /// </summary>
        [JsonProperty("collects")]
        public int CollectCount { get; set; }

        /// <summary>
        /// 人物信息
        /// </summary>
        [JsonProperty("info")]
        public PersonInformation Information { get; set; }
    }

    /// <summary>
    /// 代表人物的额外信息
    /// </summary>
    public class PersonInformation
    {
        /// <summary>
        /// 生日信息
        /// </summary>
        [JsonProperty("birth")]
        public string Birthday { get; set; } // TODO: 可考虑转换为DateTime

        /// <summary>
        /// 人物别名。键为别名类型，值为别名内容
        /// </summary>
        [JsonProperty("alias")]
        public IReadOnlyDictionary<string, string> Aliases { get; set; }

        /// <summary>
        /// 人物性别
        /// </summary>
        [JsonConverter(typeof(GenderConverter))]
        public bool? Gender { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// 身高描述
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 体重描述
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 三围
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
        [JsonProperty("main_id")]
        public uint MainID { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// 讨论上次更新的日期
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset LastPost { get; set; }

        /// <summary>
        /// 回复数
        /// </summary>
        [JsonProperty("replies")]
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
    /// 日志文章
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日志内容简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 日志中的第一张图的地址
        /// </summary>
        [JsonProperty("image")]
        public string ThumbImage { get; set; } // XXX: 第一张图的链接，考虑略去？

        /// <summary>
        /// 回复数
        /// </summary>
        [JsonProperty("replies")]
        public int ReplyCount { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTimeOffset Timestamp { get; set; }

        // 这个字段可以从Timestamp生成
        // public string DateLine { get; set; }

        /// <summary>
        /// 发布的用户
        /// </summary>
        public User User { get; set; }
    }
}
