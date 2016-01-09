using System;
using System.Linq;
using System.Text;

namespace TwoCents.Azure.Library.Validation
{
    public class Validator
    {

        private StringBuilder errorStringBuilder;
        private string error;

        public Validator()
        {
            errorStringBuilder = new StringBuilder();
        }

        public void IsRequiredString(string field, string value)
        {

            if (String.IsNullOrEmpty(value))
            {
                error = String.Format("{0} is required", field);
                AddError(error);
            }

        }

        public void IsRequiredInteger(string field, int? value)
        {

            if (value == null)
            {
                error = String.Format("{0} is required", field);
                AddError(error);
            }

        }

        public void IsValidStringEnumValue(string field, string value, string[] validValues)
        {

            if (!validValues.Any(s => s.IndexOf(value.ToUpper(), StringComparison.CurrentCultureIgnoreCase) > -1))
            {
                string error = String.Format("{0} doesn't have a valid enumeration value. Valid values are: {1}", field, string.Join(",", validValues));
                AddError(error);
            }

        }

        public void IsValidIntEnumValue(string field, int value, int[] validValues)
        {

            int result;

            result = -1;
            for (int i = 0; i < validValues.Length; i++)
            {
                if (validValues[i] == value)
                {
                    result = i;
                }
            }

            if (result == -1)
            {
                string error = String.Format("{0} doesn't have a valid enumeration value. Valid values are: {1}", field, string.Join(",", validValues));
                AddError(error);
            }

        }

        public void CheckLength(string field, string value, int length)
        {

            if (value.Length > length)
            {
                string error = String.Format("Value of {0} is longer then maximum length of {1} characters", field, length.ToString());
                AddError(error);
            }

        }

        public void AddError(string error)
        {

            if (errorStringBuilder.Length > 0)
                errorStringBuilder.Append("; " + error);
            else
                errorStringBuilder.Append(error);

        }

        public string ComposeValidationError()
        {

            if (errorStringBuilder.Length > 0)
                return errorStringBuilder.ToString();
            else
                return String.Empty;

        }

    }
}
