

function Start () {

	PlayerPrefs.SetString("SplashViewInitialize","value");
	Binding.SplashViewInitialize();
	
	PlayerPrefs.SetString("SplashView","value");


}

function OnGUI(){

if(GUI.Button(Rect(Screen.width/4, Screen.height/5, Screen.width/2, Screen.height/4),"Contents")) {

	Debug.Log("bottun pushed");
	Binding2.SplashView();

};


}
