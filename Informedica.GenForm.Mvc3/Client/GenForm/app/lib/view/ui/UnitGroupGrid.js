Ext.define('GenForm.lib.view.ui.UnitGroupGrid', {
    extend: 'Ext.grid.Panel',

    initComponent: function () {
        var me = this;

        me.itemId = 'grdUnitGroup',
        me.title = 'Eenheden',
        me.store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetUnitNames }).load();

        me.columns = [
            {
                xtype: 'gridcolumn',
                hidden: true,
                itemId: 'colId',
                dataIndex: 'Id',
                text: 'Id'
            },
            {
                xtype: 'gridcolumn',
                itemId: 'colName',
                dataIndex: 'Name',
                text: 'Eenheidgroup'
            }
        ];

        me.callParent(arguments);
    }
});
