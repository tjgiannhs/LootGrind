using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class MailSender : MonoBehaviour
{
    [SerializeField] GameObject titlos;
    [SerializeField] GameObject keimeno;
    [SerializeField] GameObject gameManager;

    TMP_InputField titlosInputField;
    TMP_InputField keimenoInputField;

    MailMessage mail;

    SoundFX soundFX;

    void Start()
    {
        mail = new MailMessage();

        titlosInputField = titlos.transform.parent.parent.GetComponent<TMP_InputField>();
        keimenoInputField = keimeno.transform.parent.parent.GetComponent<TMP_InputField>();

        soundFX = gameManager.GetComponent<SoundFX>();
    }
    public void sendMail() 
    {
        string titleText = titlosInputField.text;
        string keimenoText = keimenoInputField.text;

        if(keimenoText == "") { return; }

        mail.From = new MailAddress("skidorw@gmail.com");
        mail.To.Add("lootgrindsimulator@gmail.com");
        mail.Subject = "LGS bug report: "+titleText;
        mail.Body = keimenoText;
        // you can use others too.
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("skidorw@gmail.com", "ipfwekyylgjohfqc") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
        delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        smtpServer.Send(mail);

        //it sounds better when it plays from buttons script
        soundFX.playLinkButtonDown();

        titlosInputField.Select();
        titlosInputField.text = "";

        keimenoInputField.Select();
        keimenoInputField.text = "";

        transform.parent.parent.GetComponent<Canvas>().enabled = false;
        transform.parent.gameObject.SetActive(false);
    }
}
