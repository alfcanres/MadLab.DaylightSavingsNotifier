export enum ResultStatus {
  Success = 'Success',
  ValidationError = 'ValidationError',
  ServerError = 'ServerError'
}

export interface ServiceResult<T> {
  data: T;
  status: ResultStatus;
  messages: string[];
}