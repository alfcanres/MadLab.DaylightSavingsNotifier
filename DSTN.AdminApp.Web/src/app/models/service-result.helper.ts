import { ApiResponse } from './api-response.model';
import { ServiceResult, ResultStatus } from './service-result.model';

export function createServiceResultFromApiResponse<T>(apiResponse: ApiResponse<T>): ServiceResult<T> {
  const isValid = apiResponse.validatorResponse?.isValid ?? true;
  const messages = apiResponse.validatorResponse?.errors ?? [];
  return {
    data: apiResponse.data,
    status: isValid ? ResultStatus.Success : ResultStatus.ValidationError,
    messages
  };
}

export function createServiceResultFromError<T>(errorMessage: string): ServiceResult<T> {
  return {
    data: null as any,
    status: ResultStatus.ServerError,
    messages: [errorMessage]
  };
}