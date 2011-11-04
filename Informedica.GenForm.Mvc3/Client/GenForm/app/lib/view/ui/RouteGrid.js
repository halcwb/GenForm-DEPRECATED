Ext.define('GenForm.lib.view.ui.RouteGrid', {
    extend: 'Ext.grid.Panel',

    itemId: 'grdRoute',
    title: 'Routes',

    initComponent: function() {
        var me = this;

        me.store = Ext.create('GenForm.store.common.IdName', { directFn: GenForm.server.UnitTest.GetRouteNames }).load();

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
                text: 'Naam'
            }/*,
            {
                xtype: 'gridcolumn',
                itemId: 'colAbbreviation',
                dataIndex: 'Abbreviation',
                text: 'Afkorting'
            }*/
        ];
        me.callParent(arguments);
    }

});