using System;
using CompAndDel;

public class SaveFilter : IFilter
{
    private string name;

    public SaveFilter(string imageName)
    {
        this.name = imageName;
    }
    
    public IPicture Filter(IPicture image)
    {
        PictureProvider p = new PictureProvider();
        p.SavePicture(image, $@"imageFilter{name}.jpg");
        return image;
    }
}
