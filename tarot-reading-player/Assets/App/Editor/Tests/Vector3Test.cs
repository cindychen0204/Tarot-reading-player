using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Vector3Test
{
    [Test]
    public void CalculateVector3_Test()
    {
        var big = new Vector3(2.0f, 3.0f, 4.0f);
        var small = new Vector3(1.0f,2.0f,3.0f);

        var expectedAnswer = new Vector3(1.0f, 1.0f, 1.0f);
        var answer = big - small;

        Assert.AreEqual(expectedAnswer, answer);
        Assert.AreEqual(expectedAnswer.x, answer.x);
        Assert.AreEqual(expectedAnswer.y, answer.y);
        Assert.AreEqual(expectedAnswer.z, answer.z);
    }
}
