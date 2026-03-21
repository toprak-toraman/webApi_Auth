namespace wepAPI_denemeler.Common.Enums
{
    
    //bu projede boolean dönsekde olur ama enum kullanarak daha açıklayıcı ve genişletilebilir bir yapı kurmuş oluyoruz. İleride yeni durumlar eklemek istediğimizde enum'a yeni değerler ekleyebiliriz.
    public enum AuthResult
    {
        Success,
        UsernameTaken, 
        EmailTaken,
        InvalidCredentials,
        UserNotFound,
        InvalidPassword,
        Failure
    }
}