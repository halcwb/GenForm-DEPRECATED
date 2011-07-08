Ext.define('GenForm.store.database.Database', {
    extend: 'Ext.data.Store',
    alias: 'widget.databasestore',
    storeId: 'databasestore',

    model: 'GenForm.model.database.Database',
    autoLoad: true,

    proxy: {
        type: 'direct',
        directFn: Database.GetDatabases
    }
});