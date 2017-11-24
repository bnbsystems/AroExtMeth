Code Examples for Extension Methods:
```c#
9.8F.IsBetween(9, 10); 
 //determines whether the number 9.8 is between 9 and 10
             
DateTime.Now.IsWeekend(); 

"4AAB7F67-A898-442a-884A-364411D584EC".IsValidGuid(); 
//Checks if string is in GUID format

 "mymail@zzzzzzz.com". IsValidEmailAddress(); 
//Chacks if string is in mail format

string words = "Some words about Library";
 words.Reverse();                          
 //  returns "yrarbiL tuoba sdrow emoS"

words.GetRightSideOfString(13);        
// returns "about Library"

 new Exception("0", new Exception("1", new Exception("2", new Exception("3")))) . GetMostInner();        //returns Exception nr "3"

var numbers = 1.IterateTo(10);
 //same as new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};

 numbers.IndexOf(6);            
 //returns index of item in collection

numbers.Repeat(2, true);     
 //{1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9}

 numbers.Repeat(2, false);    
// {1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9}

 numbers.Shuffle();             
 //Randomise collections

 9.In(numbers);                  
 //Checks if 9 is in collection

numbers.ToString(";"); 
// "1; 2; 3; 4; 5; 6; 7; 8; 9"
            

var persons = new[] {
     new Person {
FirstName = "Arek",
LastName = "XYZ", 
Age = 23},
            };
 
persons.ToDataTable();
 persons.OrderBy(x => x.Age);     
//Ordering by Age


IDictionary<string, string> dictionary =
 ExpressionExt.ToDictionary(Name => "Arek",
                                       Age => "23",
                                        LastChack => DateTime.Now.ToShortDateString()); 
//Creating Dictionary by lambda style
```
 
AND MANY MORE


Now available under NuGet. Just type:
```

```
