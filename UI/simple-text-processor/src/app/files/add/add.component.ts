import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { Guid } from "guid-typescript";
import { TransferTextModel } from '../transfer-text.model'
import { TextService } from '../text.service'
import { OpenFileComponent } from '../../dialogs/open-file/open-file.component';
import { NotificationService } from '../../notification/notification.service';
import * as AppConstants from '../../app.constants';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  public addForm!: UntypedFormGroup;


  constructor(private _textService: TextService,
    private _notificationService: NotificationService,
    private _router: Router,
    private _modalService: NgbModal) { }

  ngOnInit(): void {
    this.addForm = new UntypedFormGroup({
      txt: new UntypedFormControl('', [Validators.required]),
      name: new UntypedFormControl('', [Validators.required])
    });
  }

  public submitForm = (addFormValue: any) => {
    this.executeUpload(addFormValue);
  }

  public openFileDialog = () => {
    const modalRef = this._modalService.open(OpenFileComponent, {
      size: "",
    });

    modalRef.result.then(
      (res) => {
        this.readTextFile(res);
      },
      (reason) => { }
    );
  }

  private readTextFile = (result: any) => {
    const file: File = result as File;
    if (file) {
      const fileName: string =    file.name;
      let fileReader: FileReader = new FileReader();
      fileReader.readAsText(file);
      fileReader.onload = () => {
        const content: string = fileReader.result as string;
        this.addForm.patchValue(
          {
            name: fileName.substring(0, fileName.lastIndexOf('.')) || fileName,
            txt: content
          })
      }
    }
  }

  private async executeUpload(addFormValue: any) {
    const text: string = addFormValue.txt as string;
    const name: string = addFormValue.name as string;

    const guid: string = Guid.create().toString();
    const tempFileName: string = `${name}_${guid}`;
    const size: number = new Blob([text]).size;
    const chunkSize: number = AppConstants.CHUNK_SIZE;
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
    }
    this._notificationService.showSuccess('Upload completed');
  };

  public goToFiles = () => {
    const url: string = 'list';
    this._router.navigateByUrl(url);
  };

  public hasError = (controlName: string, errorName: string) => {
    return this.addForm.controls[controlName].hasError(errorName);
  };
}