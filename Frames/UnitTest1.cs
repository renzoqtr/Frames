using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System;


namespace Frames
{
    public class Tests
    {

        private IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;// obtener el path del proyecto
            Driver = new ChromeDriver(path + @"\Drivers\"); // crear nuestro chrome driver 
        }

        [Test]
        public void Test1()
        {
            Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/iframe");
            Driver.Manage().Window.Maximize();
            Driver.SwitchTo().ParentFrame();
            var TextIframe = Driver.FindElement(By.Id("mce_0_ifr"));
            Driver.SwitchTo().Frame(TextIframe);
            var TextArea = Driver.FindElement(By.Id("tinymce"));
            TextArea.Clear();
            TextArea.SendKeys("Hello There!");
            var ContainedText = TextArea.Text;
            Assert.True(ContainedText.Equals("Hello There!"));
            Driver.SwitchTo().ParentFrame();
            var File = Driver.FindElement(By.XPath("//span[contains(., 'File')]/.."));
            File.Click();
            var NewDoc = Driver.FindElement(By.CssSelector("div[title='New document']"));
            NewDoc.Click();
            TextIframe = Driver.FindElement(By.Id("mce_0_ifr"));
            Driver.SwitchTo().Frame(TextIframe);
            TextArea = Driver.FindElement(By.Id("tinymce"));
            ContainedText = TextArea.Text;
            Assert.True(ContainedText.Equals(""));
        }

        [Test]
        public void GoToRightFrame() {
            Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/nested_frames");
            Driver.Manage().Window.Maximize();
            Driver.SwitchTo().ParentFrame();
            var FrameTop = Driver.FindElement(By.CssSelector("[name='frame-top']"));
            Driver.SwitchTo().Frame(FrameTop);
            var RightFrame = Driver.FindElement(By.CssSelector("[name='frame-right']"));
            Driver.SwitchTo().Frame(RightFrame);
            var ContainedText = Driver.FindElement(By.XPath("//body")).Text;
            Assert.IsTrue(ContainedText.Equals("RIGHT"));
        }


        [Test]
        public void GoToMiddleFrame() {
            Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/nested_frames");
            Driver.Manage().Window.Maximize();
            Driver.SwitchTo().ParentFrame();
            var Top = Driver.FindElement(By.CssSelector("[name='frame-top']"));
            Driver.SwitchTo().Frame(Top);
            var Middle = Driver.FindElement(By.CssSelector("[name='frame-middle']"));
            Driver.SwitchTo().Frame(Middle);
            var MiddleText = Driver.FindElement(By.Id("content")).Text;
            Assert.True(MiddleText.Equals("MIDDLE"));
        }


        [Test]
        public void GoToBottom()
        {
            Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/nested_frames");
            Driver.Manage().Window.Maximize();
            Driver.SwitchTo().ParentFrame();
            var BottomFrame = Driver.FindElement(By.CssSelector("[name='frame-bottom']"));
            Driver.SwitchTo().Frame(BottomFrame);
            var BodyText = Driver.FindElement(By.XPath("//body")).Text;
            Assert.True(BodyText.Equals("BOTTOM"));
        }


        [Test]
        public void GoToLeftFrame()
        {
            Driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/nested_frames");
            Driver.Manage().Window.Maximize();
            Driver.SwitchTo().ParentFrame();
            var FrameTop = Driver.FindElement(By.CssSelector("[name='frame-top']"));
            Driver.SwitchTo().Frame(FrameTop);
            var RightFrame = Driver.FindElement(By.CssSelector("[name='frame-left']"));
            Driver.SwitchTo().Frame(RightFrame);
            var ContainedText = Driver.FindElement(By.XPath("//body")).Text;
            Assert.IsTrue(ContainedText.Equals("LEFT"));
        }

        [TearDown]
        public void TearDown() { 
            Driver.Dispose();
        }
    }
}