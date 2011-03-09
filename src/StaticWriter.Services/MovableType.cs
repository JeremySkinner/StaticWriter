
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.
//  MovableType is a trademark of Six Apart, Inc.

using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace StaticWriter.Services.MovableType
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace="http://www.sixapart.com/developers/xmlrpc/")]
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
        
        [DataMember]        
        public bool isPrimary;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.sixapart.com/developers/xmlrpc/")]
    public class PostTitle
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime dateCreated;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string postid;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string userid;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string title;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.sixapart.com/developers/xmlrpc/")]
    public class TrackbackPing
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string pingTitle;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string pingURL;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string pingIP;
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://www.sixapart.com/developers/xmlrpc/")]
    public class TextFilter
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string key;
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string value;
    }

    /// <summary>
    /// 
    /// </summary>
    [ServiceContract(Namespace = "http://www.sixapart.com/developers/xmlrpc/")]
    public interface IMovableType : StaticWriter.Services.MetaWeblog.IMetaWeblog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action="mt.getCategoryList")]
        Category[] mt_getCategoryList(
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
        [OperationContract(Action = "mt.getPostCategories")]
        Category[] mt_getPostCategories(
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
        [OperationContract(Action = "mt.getRecentPostTitles")]
        PostTitle[] mt_getRecentPostTitles(
            string blogid,
            string username,
            string password,
            int numberOfPosts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <returns></returns>
        [OperationContract(Action = "mt.getTrackbackPings")]
        TrackbackPing[] mt_getTrackbackPings(
            string postid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract(Action = "mt.publishPost")]
        bool mt_publishPost(
            string postid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        [OperationContract(Action = "mt.setPostCategories")]
        bool mt_setPostCategories(
            string postid,
            string username,
            string password,
            Category[] categories);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract(Action = "mt.supportedMethods")]
        string[] mt_supportedMethods();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        [OperationContract(Action="mt.supportedTextFilters")]
        TextFilter[] mt_supportedTextFilters();
    }
}

