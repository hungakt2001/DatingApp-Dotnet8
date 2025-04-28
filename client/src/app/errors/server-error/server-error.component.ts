import { Router } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'app-server-error',
  standalone: true,
  imports: [],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css',
})
export class ServerErrorComponent {
  error: any;
  constructor(private routes: Router) {
    const navigation = this.routes.getCurrentNavigation();
    this.error = navigation?.extras?.state?.['error'];
  }
}
