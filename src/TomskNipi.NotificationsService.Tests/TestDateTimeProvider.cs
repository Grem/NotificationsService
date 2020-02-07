using System;
using System.Collections.Generic;
using NUnit.Framework;
using TomskNipi.NotificationsService.Core;

namespace TomskNipi.NotificationsService.Tests
{
    /// Grebenkovma TECHDEBT Данный класс скопирован из проекта UserServices
    /// <summary>
    /// 
    /// </summary>
    public class TestDateTimeProvider : IDateTimeProvider
    {
        public TestDateTimeProvider()
        {
            ValuesToReturn = new Queue<DateTime>();
        }

        public Queue<DateTime> ValuesToReturn { get; }


        public DateTime GetCurrentDateTime()
        {
            Assert.That(ValuesToReturn, Has.Count.GreaterThanOrEqualTo(1),
                "Вызов метода GetCurrentDateTime() не ожидался.");

            var queuedDateTime = ValuesToReturn.Dequeue();
            return queuedDateTime;
        }

        public void AssertThatNoValuesLeft(string description)
        {
            Assert.That(ValuesToReturn, Has.Count.EqualTo(0),
                $"{description}: метод GetCurrentDateTime() вызывался меньше раз, чем ожидалось тестом");
        }
    }
}