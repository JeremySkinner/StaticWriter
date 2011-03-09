
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.
//  Blogger is a trademark of Google, Inc.

using System.ServiceModel;
using System.Runtime.Serialization;

namespace StaticWriter.Services.Blogger
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.blogger.com/developers/api/1_docs/")]
    public class Category
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string categoryid;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string title;
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
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace="http://www.blogger.com/developers/api/1_docs/")]
    public class Post
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public System.DateTime dateCreated;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string userid;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string postid;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string content;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.blogger.com/developers/api/1_docs/")]
    public class UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string url;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string email;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string nickname;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string lastname;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string firstname;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.blogger.com/developers/api/1_docs/")]    
    public class BlogInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string blogid;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string url;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string blogName;
    }

    /// <summary>
    /// 
    /// </summary>
    [ServiceContract(Namespace = "http://www.blogger.com/developers/api/1_docs/")]
    public interface IBlogger
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [OperationContract(Action="blogger.deletePost")]
        bool blogger_deletePost(
            string appKey,
            string postid,
            string username,
            string password,
            bool publish);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="content"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [OperationContract(Action="blogger.editPost")]
        bool blogger_editPost(
            string appKey,
            string postid,
            string username,
            string password,
            string content,
            bool publish);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action="blogger.getCategories")]
        Category[] blogger_getCategories(
            string blogid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action = "blogger.getPost")]
        Post blogger_getPost(
            string appKey,
            string postid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="numberOfPosts"></param>
        /// <returns></returns>
        [OperationContract(Action = "blogger.getRecentPosts")]
        Post[] blogger_getRecentPosts(
            string appKey,
            string blogid,
            string username,
            string password,
            int numberOfPosts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="templateType"></param>
        /// <returns></returns>
        [OperationContract(Action = "blogger.getTemplate")]
        string blogger_getTemplate(
            string appKey,
            string blogid,
            string username,
            string password,
            string templateType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action = "blogger.getUserInfo")]
        UserInfo blogger_getUserInfo(
            string appKey,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action = "blogger.getUsersBlogs")]
        BlogInfo[] blogger_getUsersBlogs(
            string appKey,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="content"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [OperationContract(Action = "blogger.newPost")]
        string blogger_newPost(
            string appKey,
            string blogid,
            string username,
            string password,
            string content,
            bool publish);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="template"></param>
        /// <param name="templateType"></param>
        /// <returns></returns>
        [OperationContract(Action = "blogger.setTemplate")]
        bool blogger_setTemplate(
            string appKey,
            string blogid,
            string username,
            string password,
            string template,
            string templateType);
    }
}
