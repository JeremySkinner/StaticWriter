
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.


using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace StaticWriter.Services.MetaWeblog
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace="http://www.xmlrpc.com/metaWeblogApi")]
    public class Enclosure
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int length;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string type;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string url;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
    public class Source
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string name;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string url;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
    public class Post
    {
        /// <summary>Date Created in ISO 8601 format</summary>
        [DataMember]
        public DateTime dateCreated;

        /// <summary></summary>
        [DataMember]
        public string description;

        /// <summary></summary>
        [DataMember]
        public string title;

        [DataMember]
        public string[] categories;
        
        [DataMember]
        public Enclosure enclosure;
        
        /// <summary></summary>
        [DataMember]
        public string link;

        /// <summary></summary>
        [DataMember]
        public string permalink;

        [DataMember]
        public string postid;
        
        [DataMember]
        public Source source;
        
        /// <summary>Allows comment on this post.  Defined as a string because
        /// not all clients send this parameter.  Need to determine when it is 
        /// not sent so that can properly perform valid default behavior.  String
        /// is null when not sent.  Otherwise, client sends an int value.
        ///  0 - None   - Unsure what this means.  Will make this equivalent to Closed for now
        ///  1 - Open   - Allow comments
        ///  2 - Closed - No comments allowed
        /// </summary>
        [DataMember]
        public string mt_allow_comments;

        /// <summary>Currently unused.  DasBlog doesn't allow trackbacks to be turned off on a post by post basis.</summary>
        [DataMember]
        public int mt_allow_pings;
        
        /// <summary>Currently unused.</summary>
        [DataMember]
        public string mt_convert_breaks;
        
        /// <summary>Currently unused.  DasBlog only allows an excerpt to be specified.</summary>
        [DataMember]
        public string mt_text_more;
        
        /// <summary>The short description for the post that is used in some feeds and can be turned on for the main page.</summary>
        [DataMember]
        public string mt_excerpt;
        
        /// <summary>Currently unused.  DasBlog doesn't allow keywords to be specified, only categories.</summary>
        [DataMember]
        public string mt_keywords;
        
        /// <summary>Array of trackback URL's to ping.</summary>
        [DataMember]
        public string[] mt_tb_ping_urls;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
    public class CategoryInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string description;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string htmlUrl;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string rssUrl;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string title;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string categoryid;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
    public class Category
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string categoryId;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string categoryName;
    }

	/// <summary>
	/// Dedicated class for the newMediaObject method : Xas
	/// </summary>
    [DataContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
	public class MediaType
	{
		/// <summary>
		/// 
		/// </summary>
        [DataMember]
        public string name;
		/// <summary>
		/// 
		/// </summary>
        [DataMember]
		public string type;
		/// <summary>
		/// 
		/// </summary>
        [DataMember]
        public byte[] bits;
	}

    [DataContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
	public class UrlInfo
	{
        [DataMember]
        public string url;
	}
	
    /// 
    /// </summary>
    [ServiceContract(Namespace = "http://www.xmlrpc.com/metaWeblogApi")]
    public interface IMetaWeblog : StaticWriter.Services.Blogger.IBlogger
    {
        /// <summary>
        /// Updates an existing post to a designated blog using the metaWeblog API. Returns true if completed.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns>true if successful, false otherwise</returns>
        [OperationContract(Action="metaWeblog.editPost")]
        bool metaweblog_editPost(
            string postid,
            string username,
            string password,
            Post post,
            bool publish);

        /// <summary>
        ///  Retrieves a list of valid categories for a post 
        /// using the metaWeblog API. Returns the metaWeblog categories
        /// class collection.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action="metaWeblog.getCategories")]
        CategoryInfo[] metaweblog_getCategories(
            string blogid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action="metaWeblog.getPost")]
        Post metaweblog_getPost(
            string postid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="numberOfPosts"></param>
        /// <returns></returns>
        [OperationContract(Action="metaWeblog.getRecentPosts")]
        Post[] metaweblog_getRecentPosts(
            string blogid,
            string username,
            string password,
            int numberOfPosts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [OperationContract(Action="metaWeblog.newPost")]
        string metaweblog_newPost(
            string blogid,
            string username,
            string password,
            Post post,
            bool publish);
		/// <summary>
		/// newMediaObject implementation : Xas
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <param name="enc"></param>
		/// <returns></returns>
		[OperationContract(Action="metaWeblog.newMediaObject")]
		UrlInfo metaweblog_newMediaObject (object blogid, string username, string password, MediaType enc);
    }
}
