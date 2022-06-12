import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Guid } from "guid-typescript";
import { TransferTextModel } from '../transfer-text.model'
import { TextService } from '../text.service'


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  public addForm!: FormGroup;

  constructor(private _textService: TextService, private _router: Router) { }

  ngOnInit(): void {
    this.addForm = new FormGroup({
      txt: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required])
    });
  }

  public submitForm = (addFormValue: any) => {
    this.executeUpload(addFormValue);
  }

  private async executeUpload(addFormValue: any) {
    const text: string = addFormValue.txt as string;
    const name: string = addFormValue.name as string;

    const guid: string = Guid.create().toString();
    const tempFileName: string = `${name}_${guid}`;
    const size: number = new Blob([text]).size;
    const chunkSize: number = 500 * 1024;
    let start: number = 0;
    let end: number = chunkSize;

    while (start < size) {
      const chunkText: string = text.substring(start, end);
      const chunkModel: TransferTextModel = {
        name: tempFileName,
        text: chunkText,
        isLastChunk: end >= size
      }
      // upload chunk
      const uploader$ = this._textService.upload(chunkModel);
      const res = await lastValueFrom(uploader$);
      start = end;
      end = start + chunkSize;
      console.info(res);
    }
    alert('Upload completed');
  };

  public goToFiles = () => {
    const url: string = 'list';
    this._router.navigateByUrl(url);
  };

  public hasError = (controlName: string, errorName: string) => {
    return this.addForm.controls[controlName].hasError(errorName);
  };
}