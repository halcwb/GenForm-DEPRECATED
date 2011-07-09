Ext.define('GenForm.store.database.Database', {
    extend: 'Ext.data.Store',
    alias: 'widget.databasestore',
    storeId: 'databasestore',
    // This requires is necessary when Ext.Loader is enabled
    requires: ['GenForm.model.database.Database'],

    model: 'GenForm.model.database.Database',
    autoLoad: true,

    proxy: {
        type: 'direct',
        directFn: Database.GetDatabases
    }
});