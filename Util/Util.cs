using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace Sistema.Util
{
    public class Util
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string Unmask(string str)
        {
            return str.Replace(".", "").Replace("-", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace(" ", "");
        }

        public static string FormatCPFCNPJ(string str)
        {
            if (str.Length == 11)
                return Convert.ToUInt64(str).ToString(@"000\.000\.000\-00");
            else
                return Convert.ToUInt64(str).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatTelefone(string str)
        {
            if (str.Length == 11)
                return Convert.ToUInt64(str).ToString(@"(00) 00000-0000");
            else
                return Convert.ToUInt64(str).ToString(@"(00) 0000-0000");
        }        
    }

    public class AlertMessage
    {
        public const string VALIDATION_MESSAGE = "Existem campos obrigatórios não preenchidos, verifique!";
        public const string INSERT_SUCESS = "Registro inserido com sucesso";
        public const string EDIT_SUCESS = "Registro alterado com sucesso";
        public const string DELETE_SUCESS = "Registro removido com sucesso";
        public const string ERROR_MESSAGE = "Ocorreram alguns erros, verifique!";
    }

    public class FormatFlag
    {
        public static string Situacao(string flSituacao)
        {
            if (flSituacao == "C")
                return "CANCELADA";
            if (flSituacao == "P")
                return "PENDENTE";
            if (flSituacao == "G")
                return "PAGA";

            return flSituacao;
        }
    }

    public static class FlashMessage
    {
        public const string ERROR = "error";
        public const string ALERT = "alert";
        public const string INFO = "info";
        public const string SUCCESS = "success";

        [Serializable]
        public class Message
        {
            public string type { get; set; }
            public string textMessage { get; set; }
            public bool closeable { get; set; }

            public Message()
            {
                closeable = true;
                type = FlashMessage.ALERT;
            }
        }

        public static void AddFlashMessage(this HtmlHelper helper, Message message)
        {
            if (helper.ViewContext.TempData.ContainsKey(message.type))
            {
                List<Message> messages = null;
                if (helper.ViewContext.TempData[message.type].GetType() == typeof(List<Message>))
                {
                    messages = (List<Message>)helper.ViewContext.TempData[message.type];
                    messages.Add(message);
                }
                else
                {
                    messages = new List<Message> { message };
                }

                helper.ViewContext.TempData[message.type] = messages;
            }
            else
            {
                helper.ViewContext.TempData[message.type] = new List<Message> { message };
            }
        }

        public static void AddFlashMessage(this Controller controller, Message message)
        {
            if (controller.TempData.ContainsKey(message.type))
            {
                List<Message> messages = null;
                if (controller.TempData[message.type].GetType() == typeof(List<Message>))
                {
                    messages = (List<Message>)controller.TempData[message.type];
                    messages.Add(message);
                }
                else
                {
                    messages = new List<Message> { message };
                }

                controller.TempData[message.type] = messages;
            }
            else
            {
                controller.TempData[message.type] = new List<Message> { message };
            }
        }

        public static void AddFlashMessage(this HtmlHelper helper, string message, string type = FlashMessage.SUCCESS)
        {
            AddFlashMessage(helper, new Message
            {
                type = type,
                textMessage = message,
                closeable = true
            });
        }

        public static void AddFlashMessage(this Controller controller, string message, string type = FlashMessage.SUCCESS)
        {
            AddFlashMessage(controller, new Message
            {
                type = type,
                textMessage = message,
                closeable = true
            });
        }

        public static string RenderFlashMessage(this HtmlHelper helper)
        {
            string cssClass, inner;
            string result = string.Empty;
            string format = @"<div class=""{0}"">{1}</div>";

            foreach (KeyValuePair<string, object> item in helper.ViewContext.TempData)
            {
                foreach (Message message in (List<Message>)item.Value)
                {
                    cssClass = string.Empty;
                    inner = string.Empty;

                    if (message.closeable)
                    {
                        inner += @"<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">×</button>";
                        cssClass += "print ";
                    }

                    if ((item.Key == message.type) && item.Key == FlashMessage.INFO)
                    {
                        cssClass += "alert alert-info";
                    }
                    else if (item.Key == FlashMessage.ERROR)
                    {
                        cssClass += "alert alert-danger";
                    }
                    else if (item.Key == FlashMessage.SUCCESS)
                    {
                        cssClass += "alert alert-success";
                    }
                    else
                    {
                        cssClass += "alert alert-warning";
                    }

                    inner += message.textMessage;
                    result += string.Format(format, cssClass, inner);
                }
            }

            return result;
        }
    }
}
