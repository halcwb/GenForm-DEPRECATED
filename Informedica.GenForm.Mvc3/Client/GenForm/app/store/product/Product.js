Ext.define('GenForm.store.product.Product', {
    extend: 'Ext.data.Store',
    storeId: 'productstore',

    model: 'GenForm.model.product.Product',
    autoLoad: false,

    proxy: {
        type: 'direct',
        directFn: GenForm.server.Product.GetProduct,
        paramsAsHash: true,
        autoLoad: false,
        api: {
            load: GenForm.server.Product.GetProduct,
            submit: GenForm.server.Product.SaveProduct
        },
        reader: {
            type: 'json',
            root: 'data',
            idProperty: 'Id'
        }
    }
});