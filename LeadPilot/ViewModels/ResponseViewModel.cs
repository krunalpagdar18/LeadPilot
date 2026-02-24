namespace LeadPilot.ViewModels
{
    public class ResponseViewModel<T>
    {
        public bool Status { get; private set; }
        public T responseData { get; private set; }
        public string ErrorMessage {  get; private set; }
        public Exception Exception { get; private set; }

        public ResponseViewModel(T response)
        {
            this.Status = true;
            this.responseData = response;
        }

        public ResponseViewModel(string errorMessage,Exception ex) {
            this.Status = false;
            this.ErrorMessage = errorMessage;
            this.Exception = ex;
        }
    }
}
