using JacobZ.WebInterface.BangumiTv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JacobZ.WebInterface.Test.BangumiTv
{
    [TestClass]
    public class ApiCoreTests
    {
        [TestMethod]
        public async Task GetSubjectTest()
        {
            // ��֤�û��Ƿ���ȷ���л�
            void ValidateUser(User user)
            {
                Assert.IsTrue(user.ID > 0);
                Assert.IsFalse(string.IsNullOrEmpty(user.NickName));
                Assert.IsFalse(string.IsNullOrEmpty(user.UserName));
                Assert.IsTrue(user.Avatar.Large.StartsWith("http://lain.bgm.tv/pic/user/l/"));
                Assert.IsTrue(user.Avatar.Medium.StartsWith("http://lain.bgm.tv/pic/user/m/"));
                Assert.IsTrue(user.Avatar.Small.StartsWith("http://lain.bgm.tv/pic/user/s/"));
                Assert.IsNull(user.Sign);
            }

            // ����ѧ����
            var subject = await ApiCore.GetSubject(5649, false);
            Assert.AreEqual(5649u, subject.ID);
            Assert.AreEqual(SubjectType.Animation, subject.Type);
            Assert.AreEqual("��ͽ���ۆT��", subject.Name);
            Assert.AreEqual("����ѧ����", subject.ChineseName);
            Assert.AreEqual(146, subject.Summary.Length); // ͨ�������ж��ַ�����ȣ���ͬ
            Assert.AreEqual(DateTime.Parse("2010-07-03"), subject.AirDate);
            Assert.AreEqual(DayOfWeek.Saturday, subject.AirWeekday);
            Assert.AreEqual(10, subject.Rating.Length);
            Assert.IsTrue(subject.Rank > 0);

            // images
            Assert.IsTrue(subject.Images.Common.StartsWith("http://lain.bgm.tv/pic/cover/c/"));
            Assert.IsTrue(subject.Images.Grid.StartsWith("http://lain.bgm.tv/pic/cover/g/"));
            Assert.IsTrue(subject.Images.Large.StartsWith("http://lain.bgm.tv/pic/cover/l/"));
            Assert.IsTrue(subject.Images.Medium.StartsWith("http://lain.bgm.tv/pic/cover/m/"));
            Assert.IsTrue(subject.Images.Small.StartsWith("http://lain.bgm.tv/pic/cover/s/"));

            // collection
            Assert.IsTrue(subject.CollectionStats[CollectionStatus.Collect] > 0);
            Assert.IsTrue(subject.CollectionStats[CollectionStatus.Doing] > 0);
            Assert.IsTrue(subject.CollectionStats[CollectionStatus.Dropped] > 0);
            Assert.IsTrue(subject.CollectionStats[CollectionStatus.On_Hold] > 0);
            Assert.IsTrue(subject.CollectionStats[CollectionStatus.Wish] > 0);

            // eps
            Assert.IsNull(subject.EpisodeCount);
            Assert.AreEqual(13, subject.Episodes.Count);
            var episode = subject.Episodes[0];
            Assert.AreEqual(37158u, episode.ID);
            Assert.AreEqual(EpisodeType.Main, episode.Type);
            Assert.AreEqual(1, episode.Sort);
            Assert.AreEqual("�@��ľ���¤ǣ����ؾA���Τ��θФ�!?���Ȥꤢ�����Ѥ��Ǥߤ褦��", episode.Name);
            Assert.AreEqual("ӣ�����£�ÿ�ζ�������������֮�����˰�", episode.ChineseName);
            Assert.AreEqual(TimeSpan.Parse("00:24:00"), episode.Duration);
            Assert.AreEqual(DateTime.Parse("2010-07-03"), episode.AirDate);
            Assert.IsTrue(episode.CommentCount >= 22);
            Assert.AreEqual(420, episode.Description.Length);
            Assert.AreEqual(EpisodeStatus.Air, episode.Status);

            // crt
            Assert.AreEqual(9, subject.Characters.Count);
            var chara = subject.Characters.Keys.First();
            Assert.AreEqual(10957u, chara.ID);
            Assert.AreEqual("���勵���ȥ�", chara.Name);
            Assert.AreEqual("����¡��", chara.ChineseName);
            Assert.AreEqual(RoleType.Lead, chara.Role);
            Assert.IsTrue(chara.Images.Large.StartsWith("http://lain.bgm.tv/pic/crt/l/"));
            Assert.IsTrue(chara.Images.Medium.StartsWith("http://lain.bgm.tv/pic/crt/m/"));
            Assert.IsTrue(chara.Images.Small.StartsWith("http://lain.bgm.tv/pic/crt/s/"));
            Assert.IsTrue(chara.Images.Grid.StartsWith("http://lain.bgm.tv/pic/crt/g/"));
            Assert.IsTrue(chara.CommentCount >= 8);
            Assert.IsTrue(chara.CollectCount >= 38);
            Assert.IsTrue(chara.Information.Aliases.Count >= 4);
            Assert.AreEqual("Tsuda Takatoshi", chara.Information.Aliases["romaji"]);
            Assert.AreEqual(false, chara.Information.Gender);
            Assert.AreEqual(2, subject.Characters[chara].Count);
            var actor = subject.Characters[chara].First();
            Assert.AreEqual(4398u, actor.ID);
            Assert.AreEqual("С�֤椦", actor.Name);
            Assert.IsTrue(actor.Images.Large.StartsWith("http://lain.bgm.tv/pic/crt/l/"));
            Assert.IsTrue(actor.Images.Medium.StartsWith("http://lain.bgm.tv/pic/crt/m/"));
            Assert.IsTrue(actor.Images.Small.StartsWith("http://lain.bgm.tv/pic/crt/s/"));
            Assert.IsTrue(actor.Images.Grid.StartsWith("http://lain.bgm.tv/pic/crt/g/"));

            // staff
            Assert.AreEqual(8, subject.Staffs.Count);
            var staff = subject.Staffs.Keys.ElementAt(1);
            Assert.AreEqual(3768u, staff.ID);
            Assert.AreEqual("��ɺ��", staff.Name);
            Assert.AreEqual("������", staff.ChineseName);
            Assert.IsTrue(staff.Images.Large.StartsWith("http://lain.bgm.tv/pic/crt/l/"));
            Assert.IsTrue(staff.Images.Medium.StartsWith("http://lain.bgm.tv/pic/crt/m/"));
            Assert.IsTrue(staff.Images.Small.StartsWith("http://lain.bgm.tv/pic/crt/s/"));
            Assert.IsTrue(staff.Images.Grid.StartsWith("http://lain.bgm.tv/pic/crt/g/"));
            Assert.IsTrue(staff.CommentCount >= 1);
            Assert.IsTrue(staff.CollectCount >= 0);
            Assert.AreEqual("Kanazawa Hiromitsu", staff.Information.Aliases["romaji"]);
            Assert.AreEqual(false, staff.Information.Gender);
            Assert.AreEqual("10��16��", staff.Information.Birthday);
            Assert.AreEqual(2, subject.Staffs[staff].Count);

            // topic
            Assert.IsTrue(subject.Topics.Count >= 2);
            var topic = subject.Topics[0];
            Assert.IsTrue(topic.ID > 0);
            Assert.IsTrue(topic.MainID > 0);
            Assert.IsFalse(string.IsNullOrEmpty(topic.Title));
            Assert.IsTrue(topic.Timestamp > DateTimeOffset.MinValue);
            Assert.IsTrue(topic.LastPost > DateTimeOffset.MinValue);
            Assert.IsTrue(topic.ReplyCount >= 0);
            ValidateUser(topic.User);

            // blog
            Assert.IsTrue(subject.Blogs.Count >= 2);
            var blog = subject.Blogs[0];
            Assert.IsTrue(blog.ID > 0);
            Assert.IsFalse(string.IsNullOrEmpty(blog.Title));
            Assert.IsTrue(blog.Timestamp > DateTimeOffset.MinValue);
            Assert.IsTrue(blog.ReplyCount >= 0);
            ValidateUser(blog.User);

            subject = await ApiCore.GetSubject(5649, true);
            Assert.AreEqual(5649u, subject.ID);
            Assert.AreEqual(SubjectType.Animation, subject.Type);
            Assert.AreEqual("��ͽ���ۆT��", subject.Name);
            Assert.AreEqual("����ѧ����", subject.ChineseName);
            Assert.AreEqual(146, subject.Summary.Length);
            Assert.AreEqual(13, subject.EpisodeCount);
            Assert.IsNull(subject.Episodes);

            // С�ּҵ�Ů���� 
            //await ApiCore.GetSubject(179949, true);
            //await ApiCore.GetSubject(179949, false);
        }
    }
}