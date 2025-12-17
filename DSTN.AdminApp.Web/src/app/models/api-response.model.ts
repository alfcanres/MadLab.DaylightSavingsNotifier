import { ApiValidationResponse } from "./api-validation-response.model";

export interface ApiResponse<T> {
  data: T;
  validatorResponse: ApiValidationResponse;
}