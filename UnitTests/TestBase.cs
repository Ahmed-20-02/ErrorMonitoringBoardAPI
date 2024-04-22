namespace UnitTests
{
    using Moq;
    using Moq.AutoMock;

    public abstract class TestBase<T> where T : class
    {
        protected AutoMocker AutoMocker = new AutoMocker(); //creates the Automocker instance that all implementing classes use

        protected string ExceptionMessage = "Something went wrong";
        
        public TestBase()
        {
        }

        protected T CreateTestSubject() //really handy method, allows you to easily create test subjects
        {
            return this.AutoMocker.CreateInstance<T>();
        }

        public Times GetTimesVerify(int countVerify)
        {
            if (countVerify > 0)
                return Times.AtLeast(countVerify);

            return Times.Never();
        }
    }
}