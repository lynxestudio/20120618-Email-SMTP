using System;
using Gtk;
using TestMail;
using System.Collections.Generic;

public partial class MainWindow : Gtk.Window
{
public MainWindow () : base(Gtk.WindowType.Toplevel)
{
Build ();
}

protected void OnDeleteEvent (object sender, DeleteEventArgs a)
{
Application.Quit ();
a.RetVal = true;
}
protected virtual void OnBtnSubmitClicked (object sender, System.EventArgs e)
{
try{
SendMail sendMail = null;
var email = new EMailMessage(txtSubject.Text,
	                                      txtFrom.Text,
	                                      GetAddresses(),
	                                      txtDescription.Buffer.Text,
	                             chkIsHtml.Active);
if(chkCredentials.Active)
	 sendMail = new SendMail(txtHost.Text,Convert.ToInt32(txtPort.Text),chkEnableSSL.Active);
else
	 sendMail = new SendMail(txtHost.Text,Convert.ToInt32(txtPort.Text),chkEnableSSL.Active
		                            ,txtUserName.Text,txtPassword.Text);
sendMail.Send(email);
ShowMessageBox("Email Sent");
}catch(Exception ex){
ShowMessageBox(ex.Message);
}
}

void ShowMessageBox(string msg){
	using (Dialog dialog = new MessageDialog (this,
						  DialogFlags.Modal | DialogFlags.DestroyWithParent,
						  MessageType.Info,
						  ButtonsType.Ok,
						  msg)) {
		dialog.Run ();
		dialog.Hide ();
	}
}

List<string> GetAddresses() {
    var resp = new List<string>();
    foreach (string s in txtTo.Text.Split(','))
        resp.Add(s);
    return resp;
} 
protected virtual void OnBtnQuitClicked (object sender, System.EventArgs e)
{
Application.Quit();
}	

protected virtual void OnChkCredentialsToggled (object sender, System.EventArgs e)
{
tblCredentials.Visible = !chkCredentials.Active;
}
}
