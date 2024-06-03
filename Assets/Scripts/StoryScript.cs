using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class StoryScript : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private GameObject _canvas;
    private string Text;
    string TextDiolog = "— Рассказ мой начинается в 1415 году с события, позже названного Волной Гуся. Никто\nдоподлинно не знал, что произошло, версии ходили разные: Божья кара, древняя черная,\nдревние твари из мира под землей. Повод известен был наверняка: сожжение знаменитого в\nнароде чешского проповедника Яна Гуса на Констанцском соборе. А последствия…\nпоследствия были настолько странными, что слухи о них ходили по всей Европе еще очень\nдолго. Волна заставила чешских гусей увеличиться до размера человеческого и осознать\nсебя. Новообразованная гуситская община объединилась вокруг веры в Бога-Гуся.\nОкружающие Чехию католические правители, крепко выпив и хорошенько обдумав все\nпроизошедшее, объявили ей религиозную войну. В процессе же объединения гусей, однако,\nпроизошел разлад. Радикалы откололись от умеренных и ушли на гору Табор, став\nтаборитами. Лидером их быстро стал слепой гусь Ян по прозвищу Жижка, полученному за\nлюбовь к крови поверженных католиков, которых он сумел дважды прогнать из Чехии.\nОднако ж и Табор не просуществовал единым долго: «Страшный слепец» покинул его со\nсвоими сторонниками, да, к сожалению, быстро умер, оставив последователей «сиротками»,\nкак они себя далее и называли. Это была присказка, самое интересное начинается далее.\n— Эй, трактирщик, плесни еще пива!\n— В начале 1425 года появился в окрестностях Кутна-Горы молодой воин, служивший\nранее гейтманом в войске Табора. Говорил он складно, выглядел боевито, ну и потянулись к\nнему гуси со всех окрестных деревень, образовав свой лагерь и начав путешествовать по\nЧехии в поисках славы, веры и богатства. И вот, их приключения начались.";

    // Start is called before the first frame update
    public void ShowStory()
    {
        
        _canvas.SetActive(true);
        StartCoroutine(TextAnimation());
        if (Console.ReadKey().Key == ConsoleKey.Backspace)
        {
            StopAllCoroutines();
            _storyText.text = TextDiolog;
        }
    }
    
    
    private IEnumerator TextAnimation()
    {
        foreach (var ABC in TextDiolog)
        {
            _storyText.text += ABC;
            Text = ABC.ToString();

            
            if (Text is "." or "," or "!" or "?")
                yield return new WaitForSeconds(0.2f);
            else
                yield return new WaitForSeconds(0.035f);
        }
        Thread.Sleep(5000);
        HideStory();
        
    }
    
    public void HideStory()
    {
        _canvas.SetActive(false);  
    }
}
