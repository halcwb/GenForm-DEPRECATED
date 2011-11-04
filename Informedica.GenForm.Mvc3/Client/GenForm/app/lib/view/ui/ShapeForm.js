Ext.define('GenForm.lib.view.ui.ShapeForm', {
    extend: 'GenForm.lib.view.form.FormBase',

    height: 326,
    width: 460,
    bodyPadding: 10,

    initComponent: function() {
        var me = this;

        me.items = [
            {
                xtype: 'fieldset',
                itemId: 'flsShapeDetails',
                title: 'Vorm Details',
                items: [
                    {
                        xtype: 'hiddenfield',
                        itemId: 'fldId',
                        name: 'Id',
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        itemId: 'fldName',
                        name: 'Name',
                        fieldLabel: 'Naam',
                        anchor: '100%'
                    }
                ]
            },
            {
                xtype: 'tabpanel',
                activeTab: 0,
                layout: 'anchor',
                anchor: '100%',
                maintainFlex: true,
                listeners: {
                    tabChange: {
                        fn: me.onTabChangeLoadStore,
                        scope: me
                    }
                },
                items: [
                    me.createPackageGrid(),
                    me.createRouteGrid(),
                    me.createUnitGroupGrid()
                ]
            }
        ];
        me.callParent(arguments);
    }
});