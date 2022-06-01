import { Pipe, PipeTransform } from '@angular/core';
import { ContactType } from '../Enums/ContactType.enum';

@Pipe({
  name: 'textTransform'
})
export class TextTransformPipe implements PipeTransform {

  transform(value: string): string {
    
    const splitBy = ';'
    const splittedText = value.split(splitBy);
    var finalArr=[];
    splittedText.forEach(element => {
      var numArr = element.split("//");
      var x = ContactType[(parseInt(numArr[1]))];
      element = x + " - " + numArr[2] 
      
      finalArr.push(element);
    });

    return `${finalArr.join("\r\n | ")}`
  }

}
