﻿using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Base
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public PaginationInfoDto PaginationInfo { get; set; }
        [JsonIgnore] public int StatusCode { get; set; }

        public List<string> Errors { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data, PaginationInfoDto paginationInfo)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, PaginationInfo = paginationInfo };
        }

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}