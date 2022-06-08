import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from  '@angular/forms';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { TextModel } from '../text.model'
import { TextService } from '../text.service'

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  public addForm!: FormGroup;

  constructor(private _textService: TextService) { }

  ngOnInit(): void {
    this.addForm = new FormGroup({
      txt: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required])
    });
  }

  public submitForm = (addFormValue: any) => {
    const text: string = addFormValue.txt as string;
    const name: string = addFormValue.name as string;
    const model: TextModel = {
      name: name,
      text: text
    };

    this._textService.upload(model)
    .subscribe({
      next: (res: any) => {
        console.info(res);
      },
      error: (err: any) => {
        // Handle an error
        console.log(err);
      }
    });
  };

  public hasError = (controlName: string, errorName: string) => {
    return this.addForm.controls[controlName].hasError(errorName);
  };
}
