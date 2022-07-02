export class ErrorModel {
  errType: string = '';
  message: string = '';
  code?: number;
  notification: string = '';
  stack: string = '';
}