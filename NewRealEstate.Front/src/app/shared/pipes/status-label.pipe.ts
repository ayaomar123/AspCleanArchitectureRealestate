import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusLabel'
})
export class StatusLabelPipe implements PipeTransform {

  transform(status: boolean | undefined): string {
    const label = status ? 'Active' : 'Disabled';
    const badgeClass = status ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800';

    return `<span class="px-2 py-1 rounded text-sm font-medium ${badgeClass}">${label}</span>`;
  }

}
