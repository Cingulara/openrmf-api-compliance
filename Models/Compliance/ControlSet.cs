using System;


namespace openstig_api_compliance.Models.Compliance
{
  public class ControlSet {

    public ControlSet () {
    }
    public Guid id { get; set;}
    public string family { get; set;}
    public string number { get; set;}
    public string title { get; set;}
    public string priority { get; set;}
    public bool lowimpact { get; set;}
    public bool moderateimpact { get; set;}
    public bool highimpact { get; set;}
    public string supplementalGuidance { get; set;}

    public string subControlDescription { get; set;}
    public string subControlNumber { get; set;}
  }
}