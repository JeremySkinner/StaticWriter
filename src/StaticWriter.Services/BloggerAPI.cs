
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using StaticWriter.Services.Blogger;
using StaticWriter.Services.MetaWeblog;
using StaticWriter.Services.MovableType;

namespace StaticWriter.Services
{
    [ServiceContract]
    public interface IBloggerAPI : IMovableType { };

    [ServiceContract]
    public interface IFeed
    {
        [OperationContract, WebGet(UriTemplate = "/")]
        Rss20FeedFormatter GetFeed();
    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BloggerAPI : IBloggerAPI, IFeed
	{
        static List<MetaWeblog.Post> mwPosts = new List<MetaWeblog.Post>();

	    public BloggerAPI()
		{
       	}

       	bool IBlogger.blogger_deletePost(string appKey, string postid, string username, string password, bool publish)
		{
            int postNum;

            if (int.TryParse(postid, out postNum) &&
                 postNum > 0 && postNum <= mwPosts.Count-1)
            {
                mwPosts.RemoveAt(postNum-1);
            }
            return true;
		}

		bool IBlogger.blogger_editPost(string appKey, string postid, string username, string password, string content, bool publish)
		{
            int postNum;

            if (int.TryParse(postid, out postNum) &&
                 postNum > 0 && postNum <= mwPosts.Count )
            {
                mwPosts[postNum-1].description = content;
            }
            return true;
		}

		StaticWriter.Services.Blogger.Category[]  IBlogger.blogger_getCategories(string blogid, string username, string password)
		{
            ArrayList arrayList = new ArrayList();
            StaticWriter.Services.Blogger.Category bcat = new StaticWriter.Services.Blogger.Category();
            bcat.categoryid = "Front Page";
            bcat.description = "Front Page";
            bcat.htmlUrl = "http://localhost";
            bcat.rssUrl = "http://localhost";
            bcat.title = bcat.description;
            arrayList.Add( bcat );
            return arrayList.ToArray(typeof(StaticWriter.Services.Blogger.Category)) as StaticWriter.Services.Blogger.Category[];
		}

		StaticWriter.Services.Blogger.Post IBlogger.blogger_getPost(string appKey, string postid, string username, string password)
		{
           int postNum;

           if (int.TryParse(postid, out postNum) &&
                postNum > 0 && postNum <= mwPosts.Count )
           {
               StaticWriter.Services.Blogger.Post post = new StaticWriter.Services.Blogger.Post();
               post.content = mwPosts[postNum-1].description;
               post.dateCreated = mwPosts[postNum-1].dateCreated;
               post.postid = mwPosts[postNum-1].postid;
               return post;
           }
           return null;
		}

		StaticWriter.Services.Blogger.Post[] IBlogger.blogger_getRecentPosts(string appKey, 
														   string blogid, 
														   string username, 
														   string password, 
														   int numberOfPosts)
		{
            List<StaticWriter.Services.Blogger.Post> posts = new List<StaticWriter.Services.Blogger.Post>();
            foreach (MetaWeblog.Post mwPost in mwPosts)
            {
                StaticWriter.Services.Blogger.Post post = new StaticWriter.Services.Blogger.Post();
                post.content = mwPost.description;
                post.dateCreated = mwPost.dateCreated;
                post.postid = mwPost.postid;
                posts.Add(post);
            }
            return posts.ToArray();
		}

		string  IBlogger.blogger_getTemplate(string appKey, string blogid, string username, string password, string templateType)
		{
            return "";
		}

		StaticWriter.Services.Blogger.UserInfo  IBlogger.blogger_getUserInfo(string appKey, string username, string password)
		{
            StaticWriter.Services.Blogger.UserInfo userInfo = new StaticWriter.Services.Blogger.UserInfo();
            userInfo.firstname = "";
            userInfo.lastname  = "";
            return userInfo;
		}

		StaticWriter.Services.Blogger.BlogInfo[]  IBlogger.blogger_getUsersBlogs(string appKey, string username, string password)
		{
            BlogInfo[] blogs = new BlogInfo[1];
			BlogInfo blog = new BlogInfo();
			blog.blogid="0";
			blog.blogName="The Tiny, Tiny Service Bus Blog";
            blog.url = new Uri(OperationContext.Current.Channel.LocalAddress.Uri, "../feed").AbsoluteUri;
            blogs[0]=blog;
			return blogs;
		}

		string IBlogger.blogger_newPost(
			string appKey, 
			string blogid, 
			string username, 
			string password, 
			string content, 
			bool publish)
		{
            int newId = mwPosts.Count + 1;
            MetaWeblog.Post post = new MetaWeblog.Post();
            post.dateCreated = DateTime.Now;
            post.description = content;
            post.postid = newId.ToString();
            mwPosts.Add(post);
            return post.postid;
		}

		bool IBlogger.blogger_setTemplate(string appKey, string blogid, string username, string password, string template, string templateType)
		{
            return false;
		}

        
        // MoveableType
        MovableType.Category[] IMovableType.mt_getCategoryList(string blogid, string username, string password)
        {
            return null;
        }

        MovableType.Category[] IMovableType.mt_getPostCategories(string postid, string username, string password)
        {
            return null;
         }

        MovableType.PostTitle[] IMovableType.mt_getRecentPostTitles(string blogid, string username, string password, int numberOfPosts)
        {
            List<MovableType.PostTitle> postTitles = new List<MovableType.PostTitle>();
            foreach (MetaWeblog.Post mwPost in mwPosts)
            {
                MovableType.PostTitle post = new MovableType.PostTitle();
                post.dateCreated = DateTime.Now;
                post.title = mwPost.title;
                post.dateCreated = mwPost.dateCreated;
                post.postid = mwPost.postid;
                postTitles.Add(post);
            }
            return postTitles.ToArray();
        }

        MovableType.TrackbackPing[] IMovableType.mt_getTrackbackPings(string postid)
        {
            ArrayList arrayList = new ArrayList();
            return arrayList.ToArray(typeof(MovableType.TrackbackPing)) as MovableType.TrackbackPing[];
        }

        bool IMovableType.mt_publishPost(string postid, string username, string password)
        {
            return true;
        }

        bool IMovableType.mt_setPostCategories(string postid, string username, string password, MovableType.Category[] categories)
        {
            return false;
        }

        string[] IMovableType.mt_supportedMethods()
        {
            ArrayList arrayList = new ArrayList();
            foreach( MethodInfo method in GetType().GetMethods() )
            {
                if ( method.IsDefined(typeof(OperationContractAttribute),true) )
                {
                    OperationContractAttribute attr = method.GetCustomAttributes(typeof(OperationContractAttribute), true)[0] as OperationContractAttribute;
                    arrayList.Add(attr.Action);
                }
            }
            return arrayList.ToArray(typeof(string)) as string[];
        }

        MovableType.TextFilter[] IMovableType.mt_supportedTextFilters()
        {
            TextFilter tf = new TextFilter();
            tf.key="default";
            tf.@value ="default";
            return new MovableType.TextFilter[]{tf};
        }

        // Metaweblog
        bool IMetaWeblog.metaweblog_editPost(string postid, string username, string password, MetaWeblog.Post post, bool publish)
        {
            int postNum;

            if (int.TryParse(postid, out postNum) &&
                 postNum > 0 && postNum <= mwPosts.Count )
            {
                post.postid = postid;
                mwPosts[postNum-1] = post;
            }
            return true;
        }

        MetaWeblog.CategoryInfo[] IMetaWeblog.metaweblog_getCategories(string blogid, string username, string password)
        {
            ArrayList arrayList = new ArrayList();
            
            MetaWeblog.CategoryInfo bcat = new MetaWeblog.CategoryInfo();
                bcat.categoryid = "Front Page";
                bcat.description = "Front Page";
                bcat.htmlUrl = "http://localhost";
                bcat.rssUrl = "http://localhost";
                bcat.title = bcat.description;
                arrayList.Add( bcat );
            return arrayList.ToArray(typeof(MetaWeblog.CategoryInfo)) as MetaWeblog.CategoryInfo[];
        }

        MetaWeblog.Post IMetaWeblog.metaweblog_getPost(string postid, string username, string password)
        {
            int postNum;

            if (int.TryParse(postid, out postNum) &&
                 postNum > 0 && postNum <= mwPosts.Count )
            {
                return mwPosts[postNum-1];
            }
            return null;
        }

        MetaWeblog.Post[] IMetaWeblog.metaweblog_getRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            return mwPosts.ToArray();
        }

        string IMetaWeblog.metaweblog_newPost(string blogid, string username, string password, MetaWeblog.Post post, bool publish)
        {
            int newId = mwPosts.Count + 1;
            post.postid = newId.ToString();
            post.dateCreated = DateTime.Now;
            mwPosts.Add(post);
            return post.postid;
        }
        /// <summary>
		/// newMediaObject implementation : Xas
		/// </summary>
		/// <param name="blogid"></param>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="enc"></param>
		/// <returns></returns>
		UrlInfo IMetaWeblog.metaweblog_newMediaObject(object blogid, string username, string password, MediaType enc)
		{
			UrlInfo urlInfo = new UrlInfo();
			urlInfo.url = "";
			return urlInfo;
		}
    
        public Rss20FeedFormatter GetFeed()
        {
            List<SyndicationItem> items = new List<SyndicationItem>();
            foreach (var post in mwPosts)
            {
                var item = new SyndicationItem(post.title, post.description, post.link != null ? new Uri(post.link) : null);
                items.Add(item);
            }

            SyndicationFeed feed = new SyndicationFeed(items);
            return new Rss20FeedFormatter(feed);
        }
    }
}
