
public class Collectable
{
    public string name; // Il nodo secondario che può essere Sign oppure il collezionabile (nodo principale)
    public string answer; // La risposta alla domanda
    public string type; // Il nodo principale Italian, Art, Math 
    public string asset; // L'asset da scaricare e inizializzare nella scena
    public string explanation; // La spiegazione scaricata una volta premuto il pulsante del menu
    public string image; // L'immagine dell'asset 
    public string questions; // Le domande per l'asset

    public Collectable()
    {
        this.name = "";
        this.answer = "";
        this.type = ""; 
        this.asset = "";
        this.explanation = "";
        this.image = "";
        this.questions = "";
    } 
    public Collectable(string name, string answer, string type, string asset, string explanation, string image, string questions)
    {
        this.name = name;
        this.answer = answer;
        this.type = type;
        this.asset = asset;
        this.explanation = explanation;
        this.image = image;
        this.questions = questions;
    } 

    public string GetName(){return this.name;}// Return del nome (il nodo principale)
    public string GetAnswer(){return this.answer;}// Return della risposta alla domanda
    public string GetType(){return this.type;}// Return del tipo (Italian,Art,Math) 
    public string GetAsset(){return this.asset;}// Return dell'asset
    public string GetExplanation(){return this.explanation;}// Return della spiegazione sull'asset
    public string GetImage(){return this.image;}// Return dell'immagine assegnata all'asset
    public string GetQuestions(){return this.questions;}// Return dei quesiti asssegnati all'asset. E' una lista di string
    
}
