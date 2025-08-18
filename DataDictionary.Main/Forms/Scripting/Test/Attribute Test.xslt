<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" version="1.0" exclude-result-prefixes="msxsl">
  <xsl:output method="text" />
  <xsl:template match="/Model/Model.Attribute">
    <xsl:value-of select="AttributeTitle" /><xsl:text>: </xsl:text><xsl:value-of select="AttributeDescription" />
<xsl:text>
</xsl:text>
</xsl:template>
  <xsl:template match="/Model.Attribute">
    <xsl:value-of select="AttributeTitle" /><xsl:text>: </xsl:text><xsl:value-of select="AttributeDescription" />
</xsl:template>
</xsl:stylesheet>