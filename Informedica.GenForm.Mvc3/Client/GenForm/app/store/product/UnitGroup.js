Ext.define('GenForm.store.product.UnitGroup', {
    extend: 'Ext.data.Store',
    storeId: 'unitgroupstore',

    model: 'GenForm.model.product.UnitGroup',
    autoLoad: false,

    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetUnitGroups
    }
});