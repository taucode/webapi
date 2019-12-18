using Newtonsoft.Json;
using NHibernate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TauCode.Domain.Identities;
using TauCode.WebApi.Client.Exceptions;

namespace TauCode.WebApi.Testing
{
    public static class TestingWebApiExtensions
    {
        public static string BuildQueryString(this IDictionary<string, string> parameterDictionary)
        {
            var sb = new StringBuilder();
            var added = false;

            foreach (var pair in parameterDictionary)
            {
                if (pair.Value == null)
                {
                    continue;
                }

                if (added)
                {
                    sb.Append("&");
                }

                added = true;

                sb.Append($"{pair.Key}={pair.Value}");
            }

            return sb.ToString();
        }

        public static T ReadAs<T>(this HttpResponseMessage message)
        {
            var json = message.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public static ErrorDto ReadAsError(this HttpResponseMessage message)
        {
            return message.ReadAs<ErrorDto>();
        }

        public static ValidationErrorDto ReadAsValidationError(this HttpResponseMessage message)
        {
            return message.ReadAs<ValidationErrorDto>();
        }

        public static void DoInTransaction(this ISession session, Action action)
        {
            using (var tran = session.BeginTransaction())
            {
                action();

                tran.Commit();
            }
        }

        public static IdDto ToIdDto(this string s)
        {
            if (s == null)
            {
                return null;
            }

            return new IdDto(s);
        }

        public static IdDto ToIdDto(this IdBase id)
        {
            return new IdDto(id.Id);
        }

        public static TId ToId<TId>(this IdDto id) where TId : IdBase
        {
            var ctor = typeof(TId).GetConstructor(new[] { typeof(Guid) });
            return (TId)ctor.Invoke(new object[] { id.GetId() });
        }

        public static ValidationErrorServiceClientException ShouldHaveFailureNumber(
            this ValidationErrorServiceClientException ex,
            int expectedFailureNumber)
        {
            Assert.That(ex.Failures, Has.Count.EqualTo(expectedFailureNumber));
            return ex;
        }

        public static ValidationErrorServiceClientException ShouldContainFailure(
            this ValidationErrorServiceClientException ex,
            string expectedKey,
            string expectedCode,
            string expectedMessage)
        {
            Assert.That(ex.Failures, Does.ContainKey(expectedKey));
            var failure = ex.Failures[expectedKey];
            Assert.That(failure.Code, Is.EqualTo(expectedCode));
            Assert.That(failure.Message, Is.EqualTo(expectedMessage));

            return ex;
        }

        public static ValidationErrorDto ShouldHaveFailureNumber(
            this ValidationErrorDto validationError,
            int expectedFailureNumber)
        {
            Assert.That(validationError.Failures, Has.Count.EqualTo(expectedFailureNumber));
            return validationError;
        }

        public static ValidationErrorDto ShouldContainFailure(
            this ValidationErrorDto validationError,
            string expectedKey,
            string expectedCode,
            string expectedMessage)
        {
            Assert.That(validationError.Failures, Does.ContainKey(expectedKey));
            var failure = validationError.Failures[expectedKey];
            Assert.That(failure.Code, Is.EqualTo(expectedCode));
            Assert.That(failure.Message, Is.EqualTo(expectedMessage));

            return validationError;
        }
    }
}
