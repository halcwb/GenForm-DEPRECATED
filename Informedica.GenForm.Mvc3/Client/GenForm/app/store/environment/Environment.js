Ext.define('GenForm.store.environment.Environment', {
    extend: 'Ext.data.Store',
    storeId: 'environmentStore',
    // This requires is necessary when Ext.Loader is enabled
    requires: ['GenForm.model.environment.Environment'],

    model: 'GenForm.model.environment.Environment',
    autoLoad: true,

    proxy: {
        type: 'direct',
        directFn: GenForm.server.UnitTest.GetEnvironments
    }
});