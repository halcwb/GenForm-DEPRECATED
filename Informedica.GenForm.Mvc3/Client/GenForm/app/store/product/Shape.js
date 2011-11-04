Ext.define('GenForm.store.product.Shape', {
    extend: 'Ext.data.Store',
    storeId: 'shapestore',

    model: 'GenForm.model.product.Shape',
    autoLoad: false,

    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetShapes
    }
});