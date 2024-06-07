namespace MessageGenerator.Helper
{
    public class GeneratorHelper
    {

        public static string ReadOrCreateMarkUpFile(string filePath)
        {
            string result = null;

            Action raedFile = () =>
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    result = sr.ReadToEnd();
                }
            };

            try
            {
                raedFile();
            }
            catch (FileNotFoundException ex)
            {
                using (FileStream fs = File.Create(filePath)) ;
                raedFile();
            }

            return result;
        }

        /// <summary>
        /// 処理に必要なタグが記述されているか確認し、存在しない場合例外を投げる
        /// </summary>
        /// <param name="checkText"></param>
        /// <param name="tag"></param>
        /// <exception cref="FormatException"></exception>
        public static void CheckWritingNeedTag(string checkText, string tag)
        {
            if (!checkText.Contains(tag)) throw new FormatException(
                $"Not writing \"{tag}\" tag! \n " +
                $"---Confrimed Text--- " +
                $"\n{checkText}");
        }
    }
}
