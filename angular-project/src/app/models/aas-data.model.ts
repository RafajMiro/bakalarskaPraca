export interface AasData {
    modelType: string;
    assetInformation: {
      assetKind: string;
      globalAssetId: string;
    };
    submodels: {
      keys: { type: string; value: string; }[];
      type: string;
    }[];
    id: string;
    idShort: string;
  }
  