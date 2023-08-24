import { Pipe, PipeTransform } from '@angular/core';

@Pipe({  
  name: 'timespanToHoursMinutes'
})

export class TimeSpanToHoursMinutesPipe implements PipeTransform {
  transform(value: Date): string {    
    console.log(value);
    const hours = value.getHours();
    console.log(value.getMinutes());
    const minutes = value.getMinutes();
    return `${hours}:${minutes}`;  
  }
}