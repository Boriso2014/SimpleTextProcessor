import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'simple-text-processor';

  public goToFiles = () => {
    alert('Go to files clicked');
  };
}
