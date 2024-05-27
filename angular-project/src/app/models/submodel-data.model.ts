export interface SubmodelData {
    modelType: string;
    kind: string;
    semanticId: {
      keys: { type: string; value: string; }[];
      type: string;
    };
    id: string;
    idShort: string;
    submodelElements: {
      modelType: string;
      idShort: string;
      value: {
        modelType: string;
        idShort: string;
        value: {
          modelType: string;
          value: string;
          valueType: string;
          idShort: string;
        }[];
      }[];
    }[];
  }
  