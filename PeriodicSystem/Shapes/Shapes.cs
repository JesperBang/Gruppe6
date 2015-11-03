using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public class Shape : NotifyBase
    {
        // For a description of the Getter/Setter Property syntax ("{ get { ... } set { ... } }") see the Line class.
        // The static integer counter field is used to set the integer Number property to a unique number for each Shape object.
        private static int counter = 0;

        // The Number integer property holds a unique integer for each Shape object to identify them in the View (GUI) layer.
        // The "{ get; }" syntax describes that a private field 
        //  and default getter method should be generated.
        public int Number { get; }

        private double x = 200;
        
   
        public double X { get { return x; } set { x = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); } }

        private double y = 200;
        // The reason no string is given to the 'NotifyPropertyChanged' method is because, 
        //  it uses the compiler to get the name of the calling property, 
        //  which in this case is the name of the property that has changed.
        // A lambda expression can be given, because the 'NotifyPropertyChanged' method can get the property name from it.
        // Java:
        //  private double y;
        // 
        //  public double getY(){
        //    return y;
        //  }
        //
        //  public void setY(double value){
        //    y = value;
        //    NotifyPropertyChanged();
        //    NotifyPropertyChanged("CanvasCenterY");
        //  }
        public double Y { get { return y; } set { y = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); } }

        private double width = 100;
        // The reason no string is given to the 'NotifyPropertyChanged' method is because, 
        //  it uses the compiler to get the name of the calling property, 
        //  which in this case is the name of the property that has changed.
        // A lambda expression can be given, because the 'NotifyPropertyChanged' method can get the property name from it.
        // Java:
        //  private double width;
        // 
        //  public double getWidth(){
        //    return width;
        //  }
        //
        //  public void setWidth(double value){
        //    width = value;
        //    NotifyPropertyChanged();
        //    NotifyPropertyChanged("CanvasCenterX");
        //    NotifyPropertyChanged("CenterX");
        //  }
        public double Width { get { return width; } set { width = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterX); NotifyPropertyChanged(() => CenterX); } }

        private double height = 100;
        // The reason no string is given to the 'NotifyPropertyChanged' method is because, 
        //  it uses the compiler to get the name of the calling property, 
        //  which in this case is the name of the property that has changed.
        // A lambda expression can be given, because the 'NotifyPropertyChanged' method can get the property name from it.
        // Java:
        //  private double height;
        // 
        //  public double getHeight(){
        //    return height;
        //  }
        //
        //  public void setHeight(double value){
        //    height = value;
        //    NotifyPropertyChanged();
        //    NotifyPropertyChanged("CanvasCenterY");
        //    NotifyPropertyChanged("CenterY");
        //  }
        public double Height { get { return height; } set { height = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => CanvasCenterY); NotifyPropertyChanged(() => CenterY); } }

        // Derived properties.
        // Corresponds to making a Getter method in Java (for instance 'public int GetCenterX()'), 
        //  that does not have its own private field, but is calculated from other fields and properties. } }
        // The CanvasCenterX and CanvasCenterY derived properties are used by the Line class, 
        //  so it can be drawn from the center of one Shape to the center of another Shape.
        // NOTE: In the 02350SuperSimpleDemo these derived properties are called CenterX and CenterY, 
        //        but in this demo we need both these and derived properties for the coordinates of the Shape, 
        //        relative to the upper left corner of the Shape. This is an example of a breaking change, 
        //        that is changed during the lifetime of an application, because the requirements change.

        // A lambda expression can be given, because the 'NotifyPropertyChanged' method can get the property name from it.
        public double CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged(() => X); } }

        // A lambda expression can be given, because the 'NotifyPropertyChanged' method can get the property name from it.
        public double CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged(() => Y); } }

        // The CenterX and CenterY properties are used by the Shape animation to define the point of rotation.
        // NOTE: These derived properties are diffent from the Shape properties with the same names, 
        //        from the 02350SuperSimpleDemo, see above for an explanation.
        // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
        // Java:
        //  public double getCenterX(){
        //    return X + Width / 2;
        //  }
        public double CenterX => Width / 2;

        // Java:
        // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
        //  public double getCenterY(){
        //    return Y + Height / 2;
        //  }
        public double CenterY => Height / 2;

        // ViewModel properties.
        // These properties should be in the ViewModel layer, but it is easier for the demo to put them here, 
        //  to avoid unnecessary complexity.
        // NOTE: This breaks the seperation of layers of the MVVM architecture pattern.
        //       To avoid this a ShapeViewModel class should be created that wraps all Shape objects, 
        //        but it adds to the complexity of the ViewModel layer and this demo and a simpler solution was chosen for the demo.
        //        (this also adds a reference to the PresentationCore class library which is part of .NET, 
        //         but should not be used in the Model layer, creating an unnecessary dependency for the Model layer class library).
        //       To learn how to avoid this and create an application with a more pure MVVM architecture pattern, 
        //        please ask the Teaching Assistants.
        private bool isSelected;
        // The reason no string is given to the 'NotifyPropertyChanged' method is because, 
        //  it uses the compiler to get the name of the calling property, 
        //  which in this case is the name of the property that has changed.
        // A lambda expression can be given, because the 'NotifyPropertyChanged' method can get the property name from it.
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged(); NotifyPropertyChanged(() => SelectedColor); } }
        // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
        public Brush SelectedColor => IsSelected ? Brushes.Red : Brushes.Yellow;

        // Constructor.
        // The constructor is in this case used to set the default values for the properties.
        public Shape()
        {
            // This just means that the integer field called counter is incremented before its value is used to set the Number integer property.
            Number = ++counter;
        }

        // By overwriting the ToString() method, the default representation of the class is changed from the full namespace (Java: package) name, 
        //  to the value of the Number integer property, which is meant to be unique for each Shape object.
        // The ToString() method is inheritied from the Object class, that all classes inherit from.
        // This method uses an expression-bodied member (http://www.informit.com/articles/article.aspx?p=2414582) to simplify a method that only returns a value;
        public override string ToString() => Number.ToString();
    }
}
