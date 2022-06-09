import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { TextModel } from '../text.model'
import { TextService } from '../text.service'

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {
  public file: TextModel = new TextModel();
  public updateForm!: FormGroup;

  constructor(private _textService: TextService,
    private _router: Router,
    private _activeRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.updateForm = new FormGroup({
      txt: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required])
    });
    this.getFileContent();
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
          alert(res.message);
          console.info(res.message);
        },
        error: (err: any) => {
          // Handle an error
          console.error(err);
        }
      });
  };

  public goToFiles = () => {
    const url: string = 'list';
    this._router.navigateByUrl(url);
  };

  public hasError = (controlName: string, errorName: string) => {
    return this.updateForm.controls[controlName].hasError(errorName);
  };

  private getFileContent = () => {
    const name: string = this._activeRoute.snapshot.params['name'];
    this._textService.getFileContent(name)
      .subscribe({
        next: (res: any) => {
          this.file = res as TextModel;
          this.updateForm.patchValue(
            {
              name: this.file.name,
              txt: this.file.text
            }
          );
        },
        error: (err: any) => {
          // Handle an error
          console.error(err);
        }
      });
  }
}